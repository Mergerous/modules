using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class DOTweenAnimationStateComponent : StateComponent
    {
        [Serializable]
        private sealed class SequenceMember
        {
            public bool isJoined;
            public DOTweenAnimation animation;
        }
        
        [SerializeField] private bool isBackward;
        [SerializeField] private bool isSequence;
        [SerializeField, ShowIf(nameof(isSequence))] private SequenceMember[] members;
        [SerializeField, HideIf(nameof(isSequence))] private DOTweenAnimation animation;
        
        private void DoPlay()
        {
            if (isSequence)
            {
                BuildSequence().PlayForward();
            }
            else
            {
                animation.DOPlayForward();
            }
 
        }
        
        private void DoPlayBackwards()
        {
            if (isSequence)
            {
                BuildSequence().PlayBackwards();
            }
            else
            {
                animation.DOPlayBackwards();
            }
        }

        private Sequence BuildSequence()
        {
            Sequence sequence = DOTween.Sequence();
            foreach (SequenceMember member in members)
            {
                if (member.isJoined)
                {
                    sequence.Join(member.animation.tween);
                }
                else
                {
                    sequence.Append(member.animation.tween);
                }
            }

            return sequence;
        }

        public override void Apply()
        {
            if (isBackward)
            {
                DoPlayBackwards();
            }
            else
            {
                DoPlay();
            }
        }
    }
}
