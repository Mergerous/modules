using JetBrains.Annotations;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class ImagePresenter : Presenter
    {
        public void SetSpriteAsync(View view, AssetReferenceSprite reference)
        {
            if (reference.IsDone)
            {
                view.GetElement<ImageElement>("image").SetSprite(reference.Asset as Sprite);
            }
            else
            {
                reference.LoadAssetAsync().Task
                    .ToObservable()
                    .Subscribe(sprite => view.GetElement<ImageElement>("image").SetSprite(sprite))
                    .AddTo(disposables);
            }
        }
    }
}