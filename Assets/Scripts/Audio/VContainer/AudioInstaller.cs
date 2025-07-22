using System;
using Modules.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using AudioSettings = Modules.Audio.Settings.AudioSettings;

namespace Audio.Samples
{
    [Serializable]
    public sealed class AudioInstaller : IInstaller
    {
        [SerializeField] private AudioContainer container;
        [SerializeField] private AudioSettings audioSettings;
        
        public void Install(IContainerBuilder builder)
        {
            builder.Register<AudioManager>(Lifetime.Singleton)
                .WithParameter(container)
                .WithParameter(audioSettings);
        }
    }
}