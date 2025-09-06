using UnityEditor;

namespace Modules.Common
{
    [InitializeOnLoad]
    internal static class PlayModePostProcessor
    {
        static PlayModePostProcessor()
        {
            EditorSettings.enterPlayModeOptionsEnabled = true;
            EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;
        }

        [MenuItem("Modules/Reload Domain")]
        static void ReloadDomain()
        {
            EditorUtility.RequestScriptReload();
        }
    }
}