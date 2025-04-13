using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Views.Samples
{
    [Serializable]
    public sealed class DOTweenAnimationElement : Element
    {
        [Serializable]
        private sealed class SequenceMember
        {
            public bool isJoined;
            public DOTweenAnimation animation;
        }
        
        [SerializeField] private bool isSequence;
        [SerializeField, ShowIf(nameof(isSequence))] private SequenceMember[] members;
        [SerializeField, HideIf(nameof(isSequence))] private DOTweenAnimation animation;

        public Tween DoPlay()
        {
            if (isSequence)
            {
                var sequence = BuildSequence();
                sequence.PlayForward();
                return sequence;
            }
            
            animation.DOPlayForward();
            return animation.tween;
        }

        public Tween DoPlayBackwards()
        {
            if (isSequence)
            {
                var sequence = BuildSequence();
                sequence.PlayBackwards();
                return sequence;
            }
            
            animation.DOPlayBackwards();
            return animation.tween;
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
    }
}
