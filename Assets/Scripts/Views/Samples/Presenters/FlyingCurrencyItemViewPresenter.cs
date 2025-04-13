using System;
using DG.Tweening;
using JetBrains.Annotations;
using Modules.UI.Views;
using UnityEngine;

namespace Common.Views
{
    [UsedImplicitly]
    public sealed class FlyingCurrencyItemViewPresenter : IDisposable
    {
        private View view;

        public void Initialize(View view)
        {
            this.view = view;
        }

        public void Dispose()
        {
        }

        public void SetImage(Sprite sprite)
        {
            view.GetElement<ImageElement>("image").SetSprite(sprite);
        }

        public Tweener DoMove(Vector3 sourcePosition, Vector3 destinationPosition)
        {
            MovementElement movementElement = view.GetElement<MovementElement>("movement");
            float speed = movementElement.SpeedRange.GetRandom();
            float deviation = movementElement.XMovementDeviationRange.GetRandom();
            RectTransform viewTransform = view.GetComponent<RectTransform>();
            return DOVirtual.Float(0f, 1f, speed, t =>
            {
                viewTransform.position = Vector3.Lerp(sourcePosition, destinationPosition, t) +
                                          Vector3.left * deviation * movementElement.FlyingCurve.Evaluate(t);
            });
        }
    }
}
