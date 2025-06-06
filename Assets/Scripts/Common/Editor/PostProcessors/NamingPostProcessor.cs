using System;
using System.Collections.Generic;
using System.IO;
using Modules.Common.Extensions;
using UnityEditor;

namespace Modules.Common
{
    // public static class ExtensionNames
    // {
    //     public const string PNG = ".png";
    //     public const string JPG = ".jpg";
    //     public const string GIF = ".gif";
    //     public const string SVG = ".svg";
    //     public const string JPEG = ".jpeg";
    //     public const string FBX = ".fbx";
    // }
    //
    // public class NamingPostProcessor : AssetPostprocessor
    // {
    //     private struct Folder
    //     {
    //         public string name;
    //         public string[] extensions;
    //
    //         public Folder(string name, string[] extensions)
    //         {
    //             this.name = name;
    //             this.extensions = extensions;
    //         }
    //     }
    //     
    //     
    //     private static readonly Dictionary<string, CaseType> extensionCases = new()
    //     {
    //         [ExtensionNames.PNG] = CaseType.Snake,
    //         [ExtensionNames.JPG] = CaseType.Snake,
    //         [ExtensionNames.GIF] = CaseType.Snake,
    //         [ExtensionNames.SVG] = CaseType.Snake,
    //         [ExtensionNames.JPEG] = CaseType.Snake,
    //         [ExtensionNames.FBX] = CaseType.Pascal
    //     };
    //
    //     private static readonly List<Folder> targetFolders = new()
    //     {
    //         new("Assets/Sprites", new []
    //         {
    //             ExtensionNames.PNG, ExtensionNames.JPG, ExtensionNames.GIF, 
    //             ExtensionNames.SVG, ExtensionNames.JPEG
    //         }),
    //         new("Assets/Textures", new []
    //         {
    //             ExtensionNames.PNG, ExtensionNames.JPG, ExtensionNames.GIF, 
    //             ExtensionNames.SVG, ExtensionNames.JPEG
    //         })
    //     };
    //
    //     private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
    //         string[] movedFromAssetPaths)
    //     {
    //         foreach (string importedAsset in importedAssets)
    //         {
    //             TryRenameAsset(importedAsset);
    //         }
    //
    //         foreach (string importedAsset in movedAssets)
    //         {
    //             TryRenameAsset(importedAsset);
    //         }
    //     }
    //
    //     private static void TryRenameAsset(string assetPath)
    //     {
    //         string extenstion = Path.GetExtension(assetPath);
    //         if (extensionCases.TryGetValue(extenstion, out CaseType type))
    //         {
    //             foreach (Folder folder in targetFolders)
    //             {
    //                 if (Array.IndexOf(folder.extensions, extenstion) > -1 && assetPath.Contains(folder.name))
    //                 {
    //                     string fileName = Path.GetFileNameWithoutExtension(assetPath);
    //                     AssetDatabase.RenameAsset(assetPath, fileName.ToCase(type));
    //                     return;
    //                 }
    //             }
    //         }
    //     }
    // }
}
