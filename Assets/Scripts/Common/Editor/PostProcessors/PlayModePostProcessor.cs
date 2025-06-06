using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
internal static class PlayModePostProcessor
{
    static PlayModePostProcessor()
    {
        EditorSettings.enterPlayModeOptionsEnabled = true;
        EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;
        EditorApplication.update += OnEditorUpdate;
    }

    [MenuItem("Modules/Reload Domain")]
    static void ReloadDomain()
    {
        EditorUtility.RequestScriptReload();
    }
    
    // TODO REMOVE COMPLETELY
    private static void OnEditorUpdate () 
    {
        // if (EditorApplication.isPlaying && EditorApplication.isCompiling) 
        // {
        //     Debug.Log ("Exiting play mode due to script compilation.");
        //     EditorApplication.isPlaying = false;
        // }
    }
}