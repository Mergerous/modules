using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class MovementElement : Element
    {
        [field: SerializeField] public Vector2 SpeedRange { get; private set; } = new(0.1f, 0.3f);
        [field: SerializeField] public Vector2 XMovementDeviationRange { get; private set; }
        [field: SerializeField] public AnimationCurve FlyingCurve { get; private set; }
        // [field: SerializeField] public SpriteRenderer CurrencyImage { get; private set; }
    }
}
