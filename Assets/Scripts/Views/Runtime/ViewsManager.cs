using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class ViewsManager : IHandleFactory, IViewFactory
    {
        private readonly ViewsContainer viewsContainer;
        private readonly ViewsSettings viewsSettings;
        private readonly Dictionary<string, ViewHandle> cachedHandles = new();

        public ViewsManager(ViewsSettings viewsSettings, ViewsContainer viewsContainer)
        {
            this.viewsSettings = viewsSettings;
            this.viewsContainer = viewsContainer;
        }
        
        IViewHandle IHandleFactory.CreateHandle(string key, bool shouldCache = false)
        {
            if (shouldCache)
            {
                if (cachedHandles.TryGetValue(key, out ViewHandle handle))
                {
                    return handle;
                }
                
                handle = new ViewHandle(key, CreateView, DestroyView);
                cachedHandles.Add(key, handle);
                return handle;
            }
            
            return new ViewHandle(key, CreateView, DestroyView);
        }

        public View CreateView(string key)
        {
            if (viewsSettings.ViewPrefabs.TryGetValue(key, out View prefab))
            {
               return Object.Instantiate(prefab, viewsContainer.Parent);
            }

            throw new ArgumentOutOfRangeException();
        }
        
        public void DestroyView(View view)
        {
            Object.Destroy(view.gameObject);
        }
    }
}
