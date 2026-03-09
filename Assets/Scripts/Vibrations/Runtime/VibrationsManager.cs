using JetBrains.Annotations;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace Modules.Vibrations
{
    [UsedImplicitly]
    public sealed class VibrationsManager
    {
        private bool isEnabled = true;

        public void Enable(bool isEnabled)
        {
            this.isEnabled = isEnabled;
        }
        
        public void Play(HapticPatterns.PresetType type)
        {
            if (isEnabled)
            {
                HapticPatterns.PlayPreset(type);
            }
        }
    }
}
