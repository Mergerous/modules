using JetBrains.Annotations;
using Modules.Audio;

namespace Settings
{
    [UsedImplicitly]
    public sealed class SettingsSoundsController
    {
        private readonly AudioManager audioManager;
        private readonly ISoundsContent soundsContent;
        
        public bool IsSoundsEnabled => soundsContent.IsSoundsEnabled;

        public SettingsSoundsController(AudioManager audioManager, ISoundsContent soundsContent)
        {
            this.audioManager = audioManager;
            this.soundsContent = soundsContent;
            
            audioManager.EnableSounds(IsSoundsEnabled);
        }

        public void EnableSounds(bool isEnabled)
        {
            audioManager.EnableSounds(isEnabled);
            soundsContent.EnableSounds(isEnabled);
        }
    }
}
