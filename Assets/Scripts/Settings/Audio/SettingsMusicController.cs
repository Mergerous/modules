using JetBrains.Annotations;
using Modules.Audio;

namespace Settings
{
    [UsedImplicitly]
    public sealed class SettingsMusicController
    {
        private readonly AudioManager audioManager;
        private readonly IMusicContent musicContent;
        
        public bool IsMusicEnabled => musicContent.IsMusicEnabled;

        public SettingsMusicController(AudioManager audioManager, IMusicContent musicContent)
        {
            this.audioManager = audioManager;
            this.musicContent = musicContent;
            
            audioManager.EnableMusic(IsMusicEnabled);
        }

        public void EnableMusic(bool isOn)
        {
            audioManager.EnableMusic(isOn);
            musicContent.EnableMusic(isOn);
        }
    }
}
