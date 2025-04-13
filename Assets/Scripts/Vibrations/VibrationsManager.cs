using JetBrains.Annotations;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace Modules.Vibrations
{
    [UsedImplicitly]
    public sealed class VibrationsManager
    {
        public void Play(HapticPatterns.PresetType type)
        {
            HapticPatterns.PlayPreset(type);
        }
    }
}
