using JetBrains.Annotations;
using Modules.Remote;

namespace Modules.Views
{
    public enum ImageViewState
    {
        Loading = 0,
        Active  = 1
    }
    
    [UsedImplicitly]
    public sealed class ImagePresenter : Presenter
    {
        public ImageElement ImageElement { get; private set; }
        
        public async void Subscribe(View view, ISpriteContent spriteContent)
        {
            base.Subscribe();

            try
            {
                ImageElement = view.GetElement<ImageElement>("image");
                view.SetState(ImageViewState.Loading);
                spriteContent.Sprite ??= await RemoteHelper.GetSprite(spriteContent.Url, cancellationTokenSource.Token);
                view.SetState(ImageViewState.Active);
                ImageElement.SetSprite(spriteContent.Sprite);
            }
            catch
            {
                // ignored
            }
        }
    }
}