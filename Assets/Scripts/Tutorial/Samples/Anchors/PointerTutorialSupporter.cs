using System;
using JetBrains.Annotations;
using Modules.Views;

namespace Modules.Tutorial
{
    [UsedImplicitly]
    public sealed class PointerTutorialSupporter : ITutorialSupporter
    {
        private readonly AnchorManager anchorManager;
        private readonly Func<string, ViewHandle> viewFactory;

        private ViewHandle handle;

        public PointerTutorialSupporter(AnchorManager anchorManager, Func<string, ViewHandle> viewFactory)
        {
            this.anchorManager = anchorManager;
            this.viewFactory = viewFactory;
        }

        public void Support(ITutorialSupport support)
        {
            if (support is PointerTutorialSupport anchorTutorialSupport)
            {
                handle = viewFactory(anchorTutorialSupport.viewKey);
                anchorManager.GetAnchor(anchorTutorialSupport.anchorKey, 
                    anchorTransform => handle.View.GetElement<TransformElement>("pointer_transform").Transform.position = anchorTransform.position);
            }
        }

        public void Dispose()
        {
            handle.Dispose();
        }
    }
}