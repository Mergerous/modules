using System.Text.RegularExpressions;
using UnityEditor;

namespace Modules.Common
{
    public class JarPostProcessor : AssetPostprocessor
    {
        // private static readonly string names = string.Join("|", 
        //     "androidx.lifecycle.lifecycle-common-java8", 
        //     "com.google.android.gms.play-services-measurement-api",
        //     "org.jetbrains.kotlinx.kotlinx-coroutines-core",
        //     "com.google.firebase.firebase-config");
        //
        // private static readonly string pattern = @"(?:{0})-\d+\.\d+\.\d+(?:\.\d+)?";
        //
        // private void OnPreprocessAsset()
        // {
        //     if (assetImporter is PluginImporter pluginImporter
        //         && Regex.IsMatch(assetPath, string.Format(pattern, names)))
        //     {
        //         pluginImporter.SetCompatibleWithPlatform(BuildTarget.Android, false);
        //         pluginImporter.SaveAndReimport();
        //     }
        // }
    }
}
