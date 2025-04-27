using System;
using UnityEngine;

namespace Modules.Audio
{
    [Serializable]
    public class Sound
    {
        [field: SerializeField] public string SoundKey { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public float AttenuationDuration { get; private set; }
        [field: SerializeField] public AnimationCurve AttenuationCurve { get; private set; }
    }
}