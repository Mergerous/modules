using System;
using Modules.UI.Views;
using UnityEngine;
using Range = Modules.Common.Structures.Range;

namespace Common.Views
{
    [Serializable]
    public sealed class MovementElement : Element
    {
        [field: SerializeField] public Range SpeedRange { get; private set; } = new(0.1f, 0.3f);
        [field: SerializeField] public Range XMovementDeviationRange { get; private set; }
        [field: SerializeField] public AnimationCurve FlyingCurve { get; private set; }
        // [field: SerializeField] public SpriteRenderer CurrencyImage { get; private set; }
    }
}
