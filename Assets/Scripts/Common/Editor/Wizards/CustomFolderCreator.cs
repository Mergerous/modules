using System.IO;
using System.Text.RegularExpressions;
using Modules.Common.Editor.Wizards;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Common.Editor
{
    public class CustomFolderCreator : ScriptableWizard
    {
        public const string REPLACE_PATTERN = "#NAME#";
        
        [SerializeField] private string moduleName = "Module";
        [SerializeField] private TemplateSettings settings;
    
        [MenuItem("Assets/Create/Settings/Templates/Template", false, -1)]
        public static void CreateWizard()
        {
            DisplayWizard("Create Project Folders", typeof(CustomFolderCreator), "Create");
        }
    
        private void OnWizardCreate()
        {
            Object[] objects = Selection.GetFiltered<Object>(SelectionMode.Assets);

            if (objects.Length > 0)
            {
                string folderPath = AssetDatabase.GetAssetPath(objects[0]);
                if (AssetDatabase.IsValidFolder(folderPath))
                {
                    foreach(Folder folder in settings.Folders)
                    {
                        foreach (Path<Folder> path in folder.GetPath(folderPath))
                        {
                            string parentFolder = Regex.Replace(path.link, REPLACE_PATTERN, moduleName);
                            string folderName = Regex.Replace(path.content.name, REPLACE_PATTERN, moduleName);
                            AssetDatabase.CreateFolder(parentFolder, folderName);
                        }

                        foreach (Path<Script> scriptPath in folder.GetScriptPath(folderPath))
                        {
                            string newScriptPath =  Regex.Replace(scriptPath.link, REPLACE_PATTERN, moduleName);
                            string content = Regex.Replace(scriptPath.content.asset.text, REPLACE_PATTERN, moduleName);
                            File.WriteAllText(newScriptPath, content);
                        }
                    }
       
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}