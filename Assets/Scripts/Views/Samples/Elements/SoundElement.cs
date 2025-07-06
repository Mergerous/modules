using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class SoundElement : Element
    {
        [field: SerializeField] public bool CanPlay { get; set; } = true;
        [field: SerializeField] public string SoundKey { get; private set; }
    }
}