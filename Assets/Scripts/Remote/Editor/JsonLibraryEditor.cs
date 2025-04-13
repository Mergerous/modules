using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Remote.Editor
{
    [CustomEditor(typeof(JsonLibrary))]
    public class JsonLibraryEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            Button button = new Button
            {
                text = "Copy"
            };
            button.clicked += () =>
            {
                JsonLibrary target = (JsonLibrary)serializedObject.targetObject;
                Dictionary<string, string> dictionary = target.Defaults
                    .ToDictionary(json => json.GetType().GetCustomAttribute<AddToRemoteAttribute>().searchName, JsonUtility.ToJson);
                EditorGUIUtility.systemCopyBuffer = JsonConvert.SerializeObject(dictionary);
            };

            IMGUIContainer container = new IMGUIContainer();
            SerializedProperty arrayProperty = serializedObject.FindProperty(nameof(JsonLibrary.Defaults));
            ReorderableList list = new ReorderableList(serializedObject, arrayProperty)
            {
                headerHeight = 0f,
                onAddCallback = reorderableList =>
                {
                    RemoteSearchProvider instance = CreateInstance<RemoteSearchProvider>();
                    instance.OnElementSelected += type =>
                    {
                        AddToRemoteAttribute attribute = type.GetCustomAttribute<AddToRemoteAttribute>();
                        SerializedProperty property = reorderableList.serializedProperty;
                        property.InsertArrayElementAtIndex(0);
                        property.GetArrayElementAtIndex(0).managedReferenceValue = Activator.CreateInstance(type);
                        property.serializedObject.ApplyModifiedProperties();
                    };

                    SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
                        instance);
                },
                onRemoveCallback = reorderableList =>
                {
                    SerializedProperty property = reorderableList.serializedProperty;
                    property.DeleteArrayElementAtIndex(reorderableList.index);
                    property.serializedObject.ApplyModifiedProperties();
                },
                drawElementCallback = (rect, index, active, focused) =>
                {
                    SerializedProperty prop = arrayProperty.GetArrayElementAtIndex(index);
                    Type type = prop.managedReferenceValue.GetType();
                    AddToRemoteAttribute attribute = type.GetCustomAttribute<AddToRemoteAttribute>();
                    DrawHorizontal(rect,
                        labelRect =>
                        {
                            EditorGUI.LabelField(labelRect, attribute.searchName);
                        },
                        buttonRect =>
                        {
                            if (GUI.Button(buttonRect, "Edit", EditorStyles.miniButtonLeft))
                            {
#if ODIN_INSPECTOR
                                JsonWizard.Show(prop.managedReferenceValue, serializedObject.targetObject);
#else
                                JsonWizard.Show(prop);
#endif
                            }
                        },
                        copyButtonRect =>
                        {
                            if (GUI.Button(copyButtonRect, "Copy", EditorStyles.miniButtonRight))
                            {
                                EditorGUIUtility.systemCopyBuffer = JsonUtility.ToJson(prop.managedReferenceValue);
                  
                            }
                        });

                },
                elementHeightCallback = index => EditorGUIUtility.singleLineHeight
            };

            container.onGUIHandler = () =>
            {
                list.DoLayoutList();
                EditorUtility.SetDirty(serializedObject.targetObject);
            };
            
            root.Add(button);
            root.Add(container);

            return root;
        }
        
        private static void DrawHorizontal(Rect rect, params Action<Rect>[] callbacks)
        {
            float width = rect.width;
            rect.width /= callbacks.Length;
            foreach (Action<Rect> callback in callbacks)
            {
                callback?.Invoke(rect);
                rect.x += width / callbacks.Length;
            }
        }
    }
}
