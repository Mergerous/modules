using System;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class ViewsManager
    {
        private readonly ViewsContainer viewsContainer;
        private readonly ViewsSettings viewsSettings;

        public ViewsManager(ViewsSettings viewsSettings, ViewsContainer viewsContainer)
        {
            this.viewsSettings = viewsSettings;
            this.viewsContainer = viewsContainer;
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
