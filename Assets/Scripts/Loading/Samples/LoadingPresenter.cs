using System;
using JetBrains.Annotations;
using Modules.Views;
using R3;
using UnityEngine;

namespace Loading
{
    [UsedImplicitly]
    public sealed class LoadingPresenter : Presenter
    {
        private const float HALF_BAR_SPACING = 0.02f;
        
        private readonly ViewHandle handle;
        private readonly ILoadingProgress loadingProgress;
        private readonly LoadingManager loadingManager;

        public LoadingPresenter(Func<string, ViewHandle> viewFactory,
            ILoadingProgress loadingProgress, LoadingManager loadingManager)
        {
            handle = viewFactory(LoadingConstants.LOADING_VIEW_KEY);
            this.loadingProgress = loadingProgress;
            this.loadingManager = loadingManager;
        }

        public async void Subscribe(LoadingPayload payload)
        {
            base.Subscribe();

            RectTransform leftTransform = handle.View.GetElement<TransformElement>("left_transform").RectTransform;
            RectTransform rightTransform = handle.View.GetElement<TransformElement>("right_transform").RectTransform;
            SliderElement slider = handle.View.GetElement<SliderElement>("slider");
            
            leftTransform.anchorMax = new Vector2(0f, 1f);
            rightTransform.anchorMin = new Vector2(2f * HALF_BAR_SPACING, 0f);
            slider.SetValue(HALF_BAR_SPACING);
            
            loadingManager.Prepare();
            loadingProgress.ProgressObservable
                .Subscribe(progress => UpdateSlider(progress, loadingProgress.TotalProgress))
                .AddTo(disposables);
            
            await loadingManager.LoadAsync(cancellationTokenSource.Token);

            // await DOVirtual
            //     .Float(HALF_BAR_SPACING, 1f - HALF_BAR_SPACING, loadingPayload.loadingDuration, UpdateSlider)
            //     .SetEase(loadingPayload.loadingCurve)
            //     .SetLink(handle.View.gameObject)
            //     .AsyncWaitForCompletion();
            
            payload.MoveNext();
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            handle.Dispose();
        }

        private void UpdateSlider(float value, float maxValue = 1)
        {
            RectTransform leftTransform = handle.View.GetElement<TransformElement>("left_transform").RectTransform;
            RectTransform rightTransform = handle.View.GetElement<TransformElement>("right_transform").RectTransform;
            SliderElement slider = handle.View.GetElement<SliderElement>("slider");
            float normalizedValue = value / maxValue;
            
            leftTransform.anchorMax = new Vector2(normalizedValue - HALF_BAR_SPACING, 1f);
            rightTransform.anchorMin = new Vector2(normalizedValue + HALF_BAR_SPACING, 0f);
            
            slider.SetValue(normalizedValue);
        }
    }
}