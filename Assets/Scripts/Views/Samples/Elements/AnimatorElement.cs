using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class AnimatorElement : Element
    {
        [field: SerializeField] public Animator Animator { get; private set; }
    }
}