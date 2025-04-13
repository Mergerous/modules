using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.Views
{
    [CreateAssetMenu]
    public class ViewsSettings : ScriptableObject
    {
        [SerializeField] private View[] viewPrefabs;
        private Dictionary<string, View> viewPrefabsCache;

        public IReadOnlyDictionary<string, View> ViewPrefabs
            => viewPrefabsCache ??= viewPrefabs.ToDictionary(view => view.Key, view => view);
    }
}
