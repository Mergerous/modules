using System;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class VibrationElement : Element
    {
        [field: SerializeField] public HapticPatterns.PresetType HapticType { get; private set; }
        [field: SerializeField] public bool CanPlay { get; set; } = true;
    }
}