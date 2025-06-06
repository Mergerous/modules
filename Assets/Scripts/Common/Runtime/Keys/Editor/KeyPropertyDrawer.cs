using Modules.Common.Editor.Drawers;
using Modules.Common.Extensions;
using Modules.Common.Settings;
using Modules.Common.Structures;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
#if ODIN_INSPECTOR
#endif


namespace Modules.Common.Keys.Editor
{ 
    // BUG TEMPORARY
#if ODIN_INSPECTOR
    public class KeyDrawer : OdinValueDrawer<Key>
    {
        private const string DICTIONARY_KEY_DISPLAY_NAME = "Key";
        private string displayName;
        private Key currentKey;
        private bool isDirty;
        private bool isEdit;
        private string assetName;
        private string keyName;
        private string editText;
        private KeySearchProvider provider;

        protected override void Initialize()
        {
            base.Initialize();
            currentKey = ValueEntry.SmartValue;
            isDirty = true;
            provider = ScriptableObject.CreateInstance<KeySearchProvider>();
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (isDirty)
            {
                string[] guids = AssetDatabase.FindAssets($"t:{typeof(KeysSettings)}");
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    KeysSettings asset = AssetDatabase.LoadAssetAtPath<KeysSettings>(path);
                    
                    if (asset.id == currentKey.id && currentKey.value < asset.variants.Count)
                    {
                        keyName = asset.variants[currentKey.value];
                        assetName = asset.name;
                        displayName = $"{keyName} ({assetName})";
                        ValueEntry.SmartValue = currentKey;
                        ValueEntry.ApplyChanges();
                        isDirty = false;
                        break;
                    }
                }
            }

            if (provider.selectedKey != null)
            {
                isDirty = true;
                currentKey = provider.selectedKey.Value;
                ValueEntry.SmartValue = provider.selectedKey.Value;
                ValueEntry.ApplyChanges();
                provider.selectedKey = null;
            }

            GUILayout.BeginHorizontal();
            
            if (!Property.Name.IsNullOrEmpty() && Property.Name != DICTIONARY_KEY_DISPLAY_NAME)
            {
                GUILayout.Label(Property.NiceName);
            }

            if (!isEdit)
            {
                editText = $"{assetName}/{keyName}";
                if (GUILayout.Button(displayName, EditorStyles.popup))
                {
                    SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), provider);
                }
            } 
            else
            {
                editText = GUILayout.TextField(editText);

                if (GUILayout.Button("Add"))
                {
                    string[] parts = editText.Split('/', 2);
                    var guids = AssetDatabase.FindAssets($"t:{typeof(KeysSettings)} {parts[0]}");
                    foreach (string guid in guids)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(guid);
                        KeysSettings asset = AssetDatabase.LoadAssetAtPath<KeysSettings>(path);

                        if (!asset.variants.Contains(parts[1]))
                        {
                            asset.variants.Add(parts[1]);
                        }
                    }
                }

                if (GUILayout.Button("Remove"))
                {
                    string[] parts = editText.Split('/', 2);
                    var guids = AssetDatabase.FindAssets($"t:{typeof(KeysSettings)} {parts[0]}");
                    foreach (string guid in guids)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(guid);
                        KeysSettings asset = AssetDatabase.LoadAssetAtPath<KeysSettings>(path);

                        if (asset.variants.Contains(parts[1]))
                        {
                            asset.variants.Remove(parts[1]);
                        }
                    }
                }
            }
            
            isEdit = GUILayout.Toggle(isEdit, "Edit", EditorStyles.miniButton);
            
            GUILayout.EndHorizontal();
        }
    }
    
#endif
    
    [CustomPropertyDrawer(typeof(Key))]
    public class KeyPropertyDrawer : PropertyDrawer
    {
        private const string DICTIONARY_KEY_DISPLAY_NAME = "Key";
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement()
            {
                style = { flexDirection = FlexDirection.Row }
            };
            
            SerializedProperty idProperty = property.FindPropertyRelative(nameof(Key.id));
            SerializedProperty valueProperty = property.FindPropertyRelative(nameof(Key.value));

            PopupField<string> field = new PopupField<string>
            {
                style =
                {
                    height = EditorGUIUtility.singleLineHeight
                }
            };

            Toggle toggle = new Toggle("Edit");

            string[] guids = AssetDatabase.FindAssets($"t:{typeof(KeysSettings)}");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                KeysSettings asset = AssetDatabase.LoadAssetAtPath<KeysSettings>(path);

                if (asset.id == idProperty.intValue)
                {
                    field.value = $"{asset.variants[valueProperty.intValue]} ({asset.name})";
                    break;
                }
            }

            if (!property.name.IsNullOrEmpty() && property.name != DICTIONARY_KEY_DISPLAY_NAME)
            {
                field.label = property.displayName;
            }
            
            field.RegisterCallback<ClickEvent>(_ =>
            {
                KeySearchProvider instance = ScriptableObject.CreateInstance<KeySearchProvider>();
                instance.OnElementSelected += key =>
                {
                    idProperty.intValue = key.id;
                    valueProperty.intValue = key.value;
                    
                    string[] guids = AssetDatabase.FindAssets($"t:{typeof(KeysSettings)}");
                    foreach (string guid in guids)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(guid);
                        KeysSettings asset = AssetDatabase.LoadAssetAtPath<KeysSettings>(path);

                        if (asset.id == idProperty.intValue)
                        {
                            field.value = $"{asset.variants[key.value]} ({asset.name})";
                            break;
                        }
                    }
                    
                    property.serializedObject.ApplyModifiedProperties();
                };
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), instance);
            });
            
            container.Add(field);
            container.Add(toggle);
            
            return container;
        }
    }
}

