using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Views
{
    public abstract class BehaviourStateComponent<T> : StateComponent 
        where T : Behaviour
    {
        [SerializeField] protected T subject;

        [SerializeField] private BehaviourStateOptions behaviourStateOptions;
        [SerializeField, ShowIf(nameof(ChangeActivity))] private bool isEnabled;

        private bool ChangeActivity => behaviourStateOptions.HasFlag(BehaviourStateOptions.Enabled);
        
        public override void Apply()
        {
            if (ChangeActivity)
            {
                subject.enabled = isEnabled;
            }
        }
    }
}