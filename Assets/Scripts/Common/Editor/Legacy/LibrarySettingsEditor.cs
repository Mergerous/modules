using System.Collections;
using System.Reflection;
using Modules.Common.Settings;
// using Modules.Configurations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Common.Editor
{
    [CustomEditor(typeof(ScriptableLibrary))]
    public class LibrarySettingsEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            IMGUIContainer container = new IMGUIContainer();
            IList ilist = (target as ScriptableLibrary).variants;
            ReorderableList list = new ReorderableList(ilist, typeof(ScriptableObject))
            {
                elementHeight = EditorGUIUtility.singleLineHeight,
                onAddCallback = reorderableList =>
                {
                    LibrarySearchProvider instance = CreateInstance<LibrarySearchProvider>();
                    instance.OnElementSelected += type =>
                    {
                        // AddToConfigAttribute attribute = type.GetCustomAttribute<AddToConfigAttribute>();
                        // ScriptableObject factory = CreateInstance(type);
                        // factory.name = attribute.searchName;
                        // AssetDatabase.AddObjectToAsset(factory, target);
                        // AssetDatabase.SaveAssets();
                        // reorderableList.list.Add(factory);
                    };

                    SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
                        instance);
                },
                onRemoveCallback = reorderableList =>
                {
                    ScriptableObject o = reorderableList.list[reorderableList.index] as ScriptableObject;
                    reorderableList.list.RemoveAt(reorderableList.index);
                    AssetDatabase.RemoveObjectFromAsset(o);
                    AssetDatabase.SaveAssets();

                },
                drawElementCallback = (rect, index, active, focused) =>
                {
                    GUI.enabled = false;
                    EditorGUI.ObjectField(rect, ilist[index] as Object, typeof(SerializedObject));
                    GUI.enabled = true;
                }
            };


            container.onGUIHandler = () =>
            {
                list.DoLayoutList();
            };

            root.Add(container);

            return root;
        }
    }   
}
