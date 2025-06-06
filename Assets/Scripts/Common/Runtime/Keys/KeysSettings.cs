using System.Collections.Generic;
using UnityEngine;

namespace Modules.Common
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(KeysSettings), fileName = nameof(KeysSettings))]
    public class KeysSettings : ScriptableObject
    {
        public int id;
        #if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ListDrawerSettings(ShowIndexLabels = true, DraggableItems = false)]
        #endif
        [NonReorderable] public List<string> variants;
    }
}
