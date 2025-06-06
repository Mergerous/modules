using System.Collections.Generic;
using UnityEditor;

namespace Modules.Common
{
    public class KeySettingsPostProcessor : AssetPostprocessor
    {
        private const string TYPE_FILTER = "t:{0}";
        public static List<KeysSettings> settings = new();
    
        [InitializeOnLoadMethod]
        private static void Init()
        {
            foreach (string guid in AssetDatabase.FindAssets(string.Format(TYPE_FILTER, typeof(KeysSettings).FullName)))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                KeysSettings asset = AssetDatabase.LoadAssetAtPath<KeysSettings>(path);
                settings.Add(asset);
            }
        }

    
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (string asset in importedAssets)
            {
                KeysSettings keys = AssetDatabase.LoadAssetAtPath<KeysSettings>(asset);
                if (keys != null && !settings.Contains(keys))
                {
                    settings.Add(keys);
                }
            }
        }
    }
}
