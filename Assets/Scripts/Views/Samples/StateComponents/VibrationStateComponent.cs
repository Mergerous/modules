using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class VibrationStateComponent : StateComponent
    {
        [SerializeField] private bool canPlay;
        [SerializeField] private string vibrationElementKey = "vibration";
        
        public override void Apply()
        {
            View.GetElement<VibrationElement>(vibrationElementKey).CanPlay = canPlay;
        }
    }
}