using System.Collections.Generic;
using DG.Tweening;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Views
{
    [RequireComponent(typeof(ScrollRect))]
    public sealed class CustomScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private static readonly int Scroll = Animator.StringToHash("scroll");

        [SerializeField, Range(0f, 1f)] private float center = 0.5f;
        [SerializeField] private float magnetDuration = 0.5f;
        [SerializeField] private Ease magnetEase = Ease.OutBounce;
        [SerializeField] private float velocityThreshold = 60f;
        [SerializeField] private List<Animator> animators;
    
        private ScrollRect rect;
        private Tween tween;
        private bool endDragTrigger;

        public Subject<int> SelectedSubject = new();

        private void Awake()
        {
            rect = GetComponent<ScrollRect>();
        }

        private void Start()
        {
            foreach (var a in animators)
            {
                a.speed = 0f;
            }
        }

        public void AddAnimator(Animator animator)
        {
            animators.Add(animator);
            animator.speed = 0f;
        }

        [Button]
        public void SpinTo(int i)
        {
            float halfWidth = rect.viewport.rect.width / 2f;
            float offset = halfWidth / animators.Count;
            Vector2 anchoredPosition = rect.content.anchoredPosition;
            anchoredPosition.x = halfWidth * (int)(anchoredPosition.x / halfWidth) - offset * i;
         
            tween?.Kill();
            tween = DOVirtual
                .Vector2(rect.content.anchoredPosition, anchoredPosition, magnetDuration, t => rect.content.anchoredPosition = t)
                .OnComplete(() => SelectedSubject.OnNext(i))
                .SetEase(magnetEase);
        }

        public int GetSelectedIndex()
        {
            // TODO OPTIMIZE
            float halfWidth = rect.viewport.rect.width / 2f;
            float offset = halfWidth / animators.Count;
            int index = Mathf.RoundToInt(-rect.content.anchoredPosition.x % halfWidth / offset);
        
            index = Mathf.Sign(index) >= 0 ? index : animators.Count - Mathf.Abs(index);
            return index;
        }

        private void Update()
        {
            float halfWidth = rect.viewport.rect.width / 2f;
            float offset = halfWidth / animators.Count;

            for (int i = 0; i < animators.Count; i++)
            {
                float targetPosition = offset * i;
                float innerPosition = (rect.content.anchoredPosition.x + targetPosition) % halfWidth;
                float position = innerPosition / halfWidth + center;
            
                animators[i].Play(Scroll, 0, Mathf.Repeat(position, 1f));
            }
        
            if (endDragTrigger && Mathf.Abs(rect.velocity.x) < velocityThreshold)
            {
                Vector2 anchoredPosition = rect.content.anchoredPosition;
                float spins = Mathf.Sign(rect.velocity.x) > 0f 
                    ? Mathf.Ceil(rect.content.anchoredPosition.x / offset) 
                    : Mathf.Floor(rect.content.anchoredPosition.x / offset);

                endDragTrigger = false;
                rect.velocity = Vector2.zero;
            
                anchoredPosition.x = offset * spins;
                tween = DOVirtual
                    .Vector2(rect.content.anchoredPosition, anchoredPosition, magnetDuration, t => rect.content.anchoredPosition = t)
                    .OnComplete(() => SelectedSubject.OnNext(GetSelectedIndex()))
                    .SetEase(magnetEase);
            }
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            endDragTrigger = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            tween?.Kill();
        }
    }
}
