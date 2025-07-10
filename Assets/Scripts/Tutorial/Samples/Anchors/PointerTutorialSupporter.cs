using JetBrains.Annotations;
using Modules.Views;

namespace Modules.Tutorial
{
    [UsedImplicitly]
    public sealed class PointerTutorialSupporter : ITutorialSupporter
    {
        private readonly AnchorManager anchorManager;
        private readonly IHandleFactory factory;
        private IViewHandle handle;

        public PointerTutorialSupporter(AnchorManager anchorManager, IHandleFactory factory)
        {
            this.anchorManager = anchorManager;
            this.factory = factory;
        }

        public void Support(ITutorialSupport support)
        {
            if (support is PointerTutorialSupport anchorTutorialSupport)
            {
                handle = factory.CreateHandle(anchorTutorialSupport.viewKey);
                anchorManager.GetAnchor(anchorTutorialSupport.anchorKey, 
                    anchorTransform => handle.View.GetElement<TransformElement>(anchorTutorialSupport.pointerKey).Transform.position = anchorTransform.position);
            }
        }

        public void Dispose()
        {
            handle.Dispose();
        }
    }
}