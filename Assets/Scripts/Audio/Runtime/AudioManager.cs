using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.Audio.Settings;
using UnityEngine;
using AudioSettings = Modules.Audio.Settings.AudioSettings;

namespace Modules.Audio
{
    [UsedImplicitly]
    public sealed class AudioManager
    {
        private readonly AudioSettings settings;
        private readonly AudioContainer container;
        private bool soundsEnabled;
        private bool musicEnabled;
        private readonly Dictionary<string, Sound> sounds;
        private readonly Dictionary<string, Melody> melodies;
        private readonly Dictionary<string, Mixer> mixers;

        private readonly List<AudioSource> sources;
   
        public bool SoundEnabled => soundsEnabled;
        
        public AudioManager(AudioContainer container, AudioSettings settings)
        {
            this.settings = settings;
            this.container = container;
            musicEnabled = settings.IsMusicEnabled;
            soundsEnabled = settings.IsSoundsEnabled;
            
            if (settings.ConvertToDictionary)
            {
                sounds = settings.Sounds.ToDictionary(sound => sound.SoundKey);
                melodies = settings.Melodies.ToDictionary(melody => melody.Key);
                mixers = settings.Mixers.ToDictionary(mixer => mixer.Key);
            }

            sources = new List<AudioSource>();
        }
        
        public void EnableMusic(bool isEnabled)
        {
            musicEnabled = isEnabled;
            container.MelodySource.mute = !isEnabled;
        }

        public void Register(AudioSource source)
        {
            source.mute = !soundsEnabled;
            sources.Add(source);
        }

        public void EnableSounds(bool isEnabled)
        {
            soundsEnabled = isEnabled;
            container.AudioSource.mute = !isEnabled;
            
            foreach (AudioSource source in sources)
            {
                source.mute = !isEnabled;
            }
        }

        public void SetMixerGroup(string mixerKey)
        {
            if (!soundsEnabled) return;
            if (!settings.ConvertToDictionary || !mixers.TryGetValue(mixerKey, out Mixer mixer))
            {
                mixer = settings.Mixers.First(clip => clip.Key == mixerKey);
            }
            //
            // container.AudioSource.outputAudioMixerGroup = mixer.AudioMixer.outputAudioMixerGroup;
            // container.MelodySource.outputAudioMixerGroup = mixer.AudioMixer.outputAudioMixerGroup;
        }

        public void PlaySound(string soundKey, AudioSource source)
        {
            if (!soundsEnabled) return;
            if (!settings.ConvertToDictionary || !sounds.TryGetValue(soundKey, out Sound sound))
            {
                sound = settings.Sounds.Find(clip => clip.SoundKey == soundKey);
            }
            
            source.clip = sound.AudioClip;
            source.volume = 1f;
            source.Play();

            source.mute = !soundsEnabled;
            sources.Add(source);
        }

        public void PlayMelody(string melodyKey)
        {
            if (!musicEnabled) return;
            if (!settings.ConvertToDictionary || !melodies.TryGetValue(melodyKey, out Melody melody))
            {
                melody = settings.Melodies.First(clip => clip.Key == melodyKey);
            }
            container.MelodySource.clip = melody.AudioClip;
            container.MelodySource.Play();
        }
    }

}