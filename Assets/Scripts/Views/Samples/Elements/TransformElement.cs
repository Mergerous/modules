using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class TransformElement : Element
    {
        [SerializeField] private bool isRect;
        [field: SerializeField, HideIf(nameof(isRect))] public Transform Transform { get; private set; }
        [field: SerializeField, ShowIf(nameof(isRect))] public RectTransform RectTransform { get; private set; }
    }
}
