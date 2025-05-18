using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
    [Flags]
    public enum ToggleStateOptions
    {
        None    = 0,
        IsOn    = 1 << 0
    }
    
    [Serializable]
    public sealed class ToggleStateComponent : BehaviourStateComponent<Toggle>
    {
        [SerializeField] private ToggleStateOptions toggleStateOptions = ToggleStateOptions.IsOn;
        [SerializeField, ShowIf(nameof(IsOn))] private bool isOn;
        [SerializeField, ShowIf(nameof(IsOn))] private bool shouldNotify;

        private bool IsOn => toggleStateOptions.HasFlag(ToggleStateOptions.IsOn);
        
        public override void Apply()
        {
            base.Apply();
            if (shouldNotify)
            {
                subject.isOn = isOn;
            }
            else
            {
                subject.SetIsOnWithoutNotify(isOn);
            }
        }
    }
}
