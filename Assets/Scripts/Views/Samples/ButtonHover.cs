using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Samples
{
    public sealed class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private DOTweenAnimation animation;
        public void OnPointerEnter(PointerEventData eventData)
        {
            animation.DORestart();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            animation.DOPlayBackwards();
        }
    }
}