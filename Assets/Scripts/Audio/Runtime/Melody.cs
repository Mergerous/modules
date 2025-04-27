using System;
using UnityEngine;

namespace Modules.Audio
{
    [Serializable]
    public sealed class Melody
    {
        [field: SerializeField] public string Key { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
    }
}