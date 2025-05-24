using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Views
{
    public sealed class Scroll : MonoBehaviour
    {
        private const float MIN_ACCELERATION_VALUE = 0f;
        private const float MAX_ACCELERATION_VALUE = 1f;

        [Header("Options")] [SerializeField] private float targetPosition;
        [SerializeField] private float minPosition;
        [SerializeField] private float spacing = 10f;
        [SerializeField] private float speed = 100f;
        [SerializeField] private float duration;
        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

        [Header("Components")] [SerializeField] private List<RectTransform> scroll;

        private Tween scrollTween;

        public void Add(RectTransform transform)
        {
            scroll.Add(transform);
        }

        public void Clear()
        {
            scroll.Clear();
        }

        [Button]
        public void Spin(Action<RectTransform> elementStopCallback, Action<RectTransform> elementResetCallback)
        {
            scrollTween?.Kill();
            scrollTween = DOVirtual
                .Float(MAX_ACCELERATION_VALUE, MIN_ACCELERATION_VALUE, duration, acceleration =>
                {
                    Vector2 positionDelta = Vector2.right * UnityEngine.Time.deltaTime * speed * acceleration;

                    for (int i = 0; i < scroll.Count; i++)
                    {
                        RectTransform rectTransform = scroll[i];
                        if (rectTransform.anchoredPosition.x < minPosition)
                        {
                            RectTransform nextElement = scroll[i - 1 >= 0 ? i - 1 : scroll.Count - 1];
                            scroll[i].anchoredPosition = new Vector2(
                                nextElement.anchoredPosition.x
                                + nextElement.rect.width / 2f
                                + rectTransform.rect.width / 2f
                                + spacing,
                                rectTransform.anchoredPosition.y);
                            elementResetCallback?.Invoke(rectTransform);
                        }

                        scroll[i].anchoredPosition -= positionDelta;
                    }
                })
                .SetEase(curve)
                .OnComplete(() =>
                {
                    RectTransform a = scroll
                        .OrderBy(i => Mathf.Abs(targetPosition - i.anchoredPosition.x))
                        .FirstOrDefault();
                    elementStopCallback?.Invoke(a);
                });
        }
    }
}
