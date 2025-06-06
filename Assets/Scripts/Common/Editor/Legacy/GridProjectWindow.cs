using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.UIElements.Cursor;
using Object = UnityEngine.Object;

namespace Modules.Common.Editor
{
    public class GridProjectWindow : EditorWindow
    {
        public string[] names =
        {
            "Managers",
            "Controllers",
            "Configs",
            "Models",
            "Systems",
            "Components",
            "Behaviours",
            "Windows",
            "Views",
            "Constants"
        };
        [MenuItem("Examples/My Editor Window")]
        public static void ShowExample()
        {
            GridProjectWindow wnd = GetWindow<GridProjectWindow>();
            wnd.Show();
        }

        private IEnumerable<string> GetAllFolders(string source)
        {
            string[] folders = AssetDatabase.GetSubFolders(source);
            foreach (string folder in folders)
            {
                Object folderObject = AssetDatabase.LoadAssetAtPath<Object>(folder);
                if (!names.Contains(folderObject.name))
                {
                    yield return folder;
                }

                foreach (string subfolder in GetAllFolders(folder))
                {
                    yield return subfolder;
                }
            }
        }
        
        private void CreateGUI()
        {
            string[] folders = 
                GetAllFolders("Assets/Scripts")
                .ToArray();

            ScrollView scrollView = new ScrollView(ScrollViewMode.Vertical);
            VisualElement headerContent = new VisualElement();
            headerContent.style.flexDirection = FlexDirection.Row;
            headerContent.style.flexGrow = 0f;
            headerContent.style.flexShrink = 0f;
            for (int i = 0; i < folders.Length; i++)
            {
                Object folder = AssetDatabase.LoadAssetAtPath<Object>(folders[i]);
                VisualElement rawContent = new VisualElement();
                rawContent.style.flexDirection = FlexDirection.Row;
                rawContent.style.flexGrow = 0f;
                scrollView.Add(rawContent);
                for (int j = 0; j < names.Length; j++)
                {
                    VisualElement columnContent = new VisualElement();
                    ToolbarButton button = new ToolbarButton();

                    if (i == 0 && j == 0)
                    {
                        button.style.width = 200f;
                        columnContent.Add(button);
                        headerContent.Add(columnContent);
                    }
                    else if (i == 0 && j != 0)
                    {
                        Label label = new Label(names[j]);
                        label.transform.rotation = Quaternion.Euler(0, 0, -90);
                        columnContent.style.height = 100f;
                        columnContent.style.maxWidth = 50f;
                        columnContent.style.alignContent = Align.Center;
                        columnContent.style.justifyContent = Justify.FlexEnd;
                        columnContent.Add(label);
                        headerContent.Add(columnContent);
                    } 
                    else if (j == 0 && i != 0)
                    {
                        string path = folder.name;
                        button.text = path;
                        button.style.width = 200f;
                        button.style.alignContent = Align.FlexEnd;
                        columnContent.Add(button);
                        button.clicked += () => Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(path);
                        rawContent.Add(columnContent);
                    }
                    else
                    {
                        string path = $"{folders[i]}/{names[j]}";
                        bool isValid = AssetDatabase.IsValidFolder(path);
                        button.text = isValid  ? "V" : "X";
                        button.style.color = isValid ? Color.green : Color.red;
                        button.clicked += () => Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(path);
                        columnContent.style.width = 50f;
                        columnContent.Add(button);
                        rawContent.Add(columnContent);
                    }
                }
            }

            rootVisualElement.Add(headerContent);
            rootVisualElement.Add(scrollView);
        }
    }
}
