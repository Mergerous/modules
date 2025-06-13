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
        }
        
        public void EnableMusic(bool isEnabled)
        {
            musicEnabled = isEnabled;
            container.MelodySource.mute = !isEnabled;
        }
        
        public void EnableSounds(bool isEnabled)
        {
            soundsEnabled = isEnabled;
            container.AudioSource.mute = !isEnabled;
        }

        // public void SetMixerGroup(string mixerKey)
        // {
            // if (!soundsEnabled) return;
            // if (!settings.ConvertToDictionary || !mixers.TryGetValue(mixerKey, out Mixer mixer))
            // {
            //     mixer = settings.Mixers.First(clip => clip.Key == mixerKey);
            // }
            //
            // container.AudioSource.outputAudioMixerGroup = mixer.AudioMixer.outputAudioMixerGroup;
            // container.MelodySource.outputAudioMixerGroup = mixer.AudioMixer.outputAudioMixerGroup;
        // }

        public void PlaySound(string soundKey, AudioSource source = null)
        {
            if (!soundsEnabled)
            {
                return;
            }
            if (!settings.ConvertToDictionary || !sounds.TryGetValue(soundKey, out Sound sound))
            {
                sound = settings.Sounds.Find(clip => clip.SoundKey == soundKey);
            }

            source ??= container.AudioSource;
            source.clip = sound.AudioClip;
            source.volume = 1f;
            source.Play();

            source.mute = !soundsEnabled;
        }

        public void PlayMelody(string melodyKey, AudioSource source)
        {
            if (!musicEnabled)
            {
                return;
            }
            if (!settings.ConvertToDictionary || !melodies.TryGetValue(melodyKey, out Melody melody))
            {
                melody = settings.Melodies.First(clip => clip.Key == melodyKey);
            }
            
            source ??= container.MelodySource;
            source.clip = melody.AudioClip;
            source.Play();
        }
    }

}