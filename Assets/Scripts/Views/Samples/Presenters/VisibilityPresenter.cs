using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class VisibilityPresenter : Presenter
    {
        private RectTransform backgroundTransform;
        private RectTransform rectTransform;
        
        public Observable<bool> IsVisibleObservable => Tick(cancellationTokenSource.Token).ToObservable();

        public void Subscribe(View backgroundView, View view)
        {
            base.Subscribe();
            backgroundTransform ??= backgroundView.GetElement<TransformElement>("transform").Transform as RectTransform;
            rectTransform ??= view.GetElement<TransformElement>("transform").Transform as RectTransform;
        }

        private async IAsyncEnumerable<bool> Tick(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
                yield return FullyContains(backgroundTransform, rectTransform);
            }
        }
        
        private static bool FullyContains(RectTransform rectTransform, RectTransform other)
        {
            if (rectTransform == null || other == null)
            {
                return false;
            }
            
            var rect = GetWorldRect(rectTransform);
            var otherRect = GetWorldRect(other);
            
            return rect.xMin <= otherRect.xMin 
                   && rect.yMin <= otherRect.yMin 
                   && rect.xMax >= otherRect.xMax 
                   && rect.yMax >= otherRect.yMax;
        }
        
        private static Rect GetWorldRect(RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Vector2 min = corners[0];
            Vector2 max = corners[2];
            Vector2 size = max - min;
 
            return new Rect(min, size);
        }
    }
}