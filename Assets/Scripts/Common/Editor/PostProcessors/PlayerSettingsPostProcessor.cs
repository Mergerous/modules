using Modules.Common.Settings;
using UnityEditor;

namespace Modules.Common.Editor.PostProcessors
{
    public static class PlayerSettingsPostProcessor
    {
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(PublishingSettings)}");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                PublishingSettings publishingSettings = AssetDatabase.LoadAssetAtPath<PublishingSettings>(path);
                PlayerSettings.companyName = publishingSettings.companyName;
                PlayerSettings.Android.keystorePass = publishingSettings.password;
                PlayerSettings.Android.keyaliasPass = publishingSettings.password;
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            }
        }
    }
}
