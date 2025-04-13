using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
    [Flags]
    public enum ButtonStateOptions
    {
        None         = 0,
        Interactable = 1,
    }
    
    [Serializable]
    public sealed class ButtonStateComponent : BehaviourStateComponent<Button>
    {
        [SerializeField] private ButtonStateOptions buttonStateOptions;
        [SerializeField, ShowIf(nameof(ChangeInteractable))] private bool isInteractable = true;
        
        private bool ChangeInteractable => buttonStateOptions.HasFlag(ButtonStateOptions.Interactable);
        
        public override void Apply()
        {
            base.Apply();
            
            if (ChangeInteractable)
            {
                subject.interactable = isInteractable;
            }
        }
    }
}