using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Common
{
    public class Tab
    {
        public event ReorderableList.AddDropdownCallbackDelegate AddCallback
        {
            add => _list.onAddDropdownCallback = value;
            remove => _list.onAddDropdownCallback = OnAddDropdownCallback;
        }

        public event ReorderableList.ElementCallbackDelegate DrawCallback
        {
            add => _list.drawElementCallback = value;
            remove => _list.drawElementCallback = DrawElementCallback;
        }

        public event ReorderableList.ElementHeightCallbackDelegate HeightCallback
        {
            add => _list.elementHeightCallback += value;
            remove => _list.elementHeightCallback -= value;
        }

        public event ReorderableList.RemoveCallbackDelegate RemoveCallback
        {
            add => _list.onRemoveCallback += value;
            remove => _list.onRemoveCallback -= value;
        }

        public event Action<GenericMenu, string, int> CreateItemCallback
        {
            add => _createItemCallback = value;
            remove => _createItemCallback = CreateItem;
        }

        private event Action<GenericMenu, string, int> _createItemCallback;

        private const string KeyName = "_key";
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
        
        private readonly SerializedProperty _flagProperty;
        private readonly SerializedProperty _contentProperty;
        private readonly AnimBool _animBool;
        private readonly ReorderableList _list;

        public SerializedProperty KeysProperty { private get; set; }

        public SerializedProperty ContentProperty => _contentProperty;

        public string[] KeysArray { get; private set; }
        
        public string Name => ObjectNames.NicifyVariableName(_contentProperty.name);


        public Tab(SerializedProperty mainProperty, SerializedProperty checkmarkProperty, UnityAction redraw = null)
        {
            _contentProperty = mainProperty;
            _flagProperty = checkmarkProperty;
            _createItemCallback = CreateItem;
            _animBool = new AnimBool(redraw) {value = _contentProperty.isExpanded};

            if (_contentProperty.isArray)
            {
                _list = new ReorderableList(_contentProperty.serializedObject, _contentProperty);
                _list.onAddDropdownCallback += OnAddDropdownCallback;
                _list.drawElementCallback += DrawElementCallback;
                _list.elementHeightCallback += ElementHeightCallback;
                _list.headerHeight = 0;
                _list.onCanAddCallback += ONCanAddCallback;
            }
        }
        
        public Tab(SerializedProperty mainProperty, UnityAction redraw = null)
        {
            _contentProperty = mainProperty;
            _createItemCallback = CreateItem;
            _animBool = new AnimBool(redraw) {value = _contentProperty.isExpanded};

            if (_contentProperty.isArray)
            {
                _list = new ReorderableList(_contentProperty.serializedObject, _contentProperty);
                _list.onAddDropdownCallback += OnAddDropdownCallback;
                _list.drawElementCallback += DrawElementCallback;
                _list.elementHeightCallback += ElementHeightCallback;
                _list.headerHeight = 0;
                _list.onCanAddCallback += ONCanAddCallback;
            }
        }
        
        private bool ONCanAddCallback(ReorderableList list)
        {
            return true; //_settings.targetObject != null;
        }

        public bool Contains(SerializedProperty serializedProperty)
        {
            return _contentProperty.name == serializedProperty.name || (_flagProperty != null &&  _flagProperty.name == serializedProperty.name);
        }

        private float ElementHeightCallback(int index)
        {
            int length = _list.serializedProperty.arraySize;

            if (length <= 0)
                return 0.0f;

            SerializedProperty iteratorProp = _list.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty endProp = iteratorProp.GetEndProperty();

            float height = 0;

            while (iteratorProp.NextVisible(true) && !SerializedProperty.EqualContents(endProp, iteratorProp))
            {
                height += (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
            }

            return height;
        }
        
        public void Draw()
        {
            EditorGUILayout.BeginHorizontal();

            if (_flagProperty != null)
            {
                var toggle = GUILayout.Toggle(_flagProperty.boolValue,
                    new GUIContent(EditorGUIUtility.IconContent(_flagProperty.boolValue
                        ? "d_scenepicking_pickable_hover"
                        : "d_scenepicking_pickable")), EditorStyles.toolbarPopup, GUILayout.Width(35));

                _flagProperty.boolValue = toggle;
            }
            
            _animBool.target =
                EditorGUILayout.BeginFoldoutHeaderGroup(_animBool.target, Name, EditorStyles.toolbarPopup);
            
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndHorizontal();

            bool faded = EditorGUILayout.BeginFadeGroup(_animBool.faded);
            _contentProperty.isExpanded = faded;
            
            if (faded)
            {
                bool isEnabled = GUI.enabled;
                if (_flagProperty != null)
                {
                    GUI.enabled = _flagProperty.boolValue;
                }
                EditorGUI.indentLevel++;

                if (_contentProperty.isArray && KeysProperty != null)
                {
                    KeysArray = new string[KeysProperty.arraySize];
                    for (int i = 0; i < KeysProperty.arraySize; i++)
                    {
                        KeysArray[i] = KeysProperty.GetArrayElementAtIndex(i).stringValue;
                    }
                    _list.DoLayoutList();
                }
                else
                {
                    EditorGUILayout.PropertyField(_contentProperty, true);
                }

                EditorGUI.indentLevel--;
                GUI.enabled = isEnabled;
            }

            EditorGUILayout.EndFadeGroup();
            EditorGUILayout.Space(5);
        }

        private void OnAddDropdownCallback(Rect buttonRect, ReorderableList reorderableList)
        {
            var menu = new GenericMenu();
            for (int i = 0; i < KeysProperty.arraySize; i++)
            {
                if (IsAvailable(i))
                {
                    _createItemCallback?.Invoke(menu, KeysProperty.GetArrayElementAtIndex(i).stringValue, i);
                }
            }

            menu.ShowAsContext();
        }


        private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty iteratorProp = _contentProperty.GetArrayElementAtIndex(index);

            SerializedProperty endProp = iteratorProp.GetEndProperty();

            while (iteratorProp.NextVisible(true) && !SerializedProperty.EqualContents(endProp, iteratorProp))
            {
                rect.height = EditorGUIUtility.singleLineHeight;
                if (iteratorProp.name == KeyName)
                {
                    var key = iteratorProp.intValue;
                    var newKey = EditorGUI.Popup(rect, key, KeysArray, EditorStyles.toolbarPopup);
                    for (int i = 0; i < _contentProperty.arraySize; i++)
                    {
                        var anotherProperty = _contentProperty.GetArrayElementAtIndex(i);
                        var anotherKey = anotherProperty.FindPropertyRelative(KeyName);
                        if (anotherKey.intValue == newKey && anotherKey != iteratorProp)
                        {
                            anotherKey.intValue = key;
                        }
                    }

                    iteratorProp.intValue = newKey;
                }
                else
                {
                    EditorGUI.PropertyField(rect, iteratorProp, true);
                }

                rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            }
        }

        private bool IsAvailable(int key)
        {
            for (int i = 0; i < _contentProperty.arraySize; i++)
            {
                var property = _contentProperty.GetArrayElementAtIndex(i);
                var comparableKey = property.FindPropertyRelative(KeyName).intValue;
                if (comparableKey == key)
                {
                    return false;
                }
            }

            return true;
        }

        private void CreateItem(GenericMenu menu, string name, int index)
        {
            menu.AddItem(new GUIContent(name), false, (n) =>
            {
                _contentProperty.InsertArrayElementAtIndex(_contentProperty.arraySize);
                Type parentType = _contentProperty.serializedObject.targetObject.GetType();
                object instance;
                if (_contentProperty.isArray)
                {
                    instance = Activator.CreateInstance(parentType
                        .GetField(_contentProperty.propertyPath, BindingAttr).FieldType.GetGenericArguments()[0]);
                }
                else
                {
                    instance = Activator.CreateInstance(parentType
                        .GetField(_contentProperty.propertyPath, BindingAttr).FieldType);
                }

                _contentProperty.GetArrayElementAtIndex(_contentProperty.arraySize - 1).managedReferenceValue =
                    instance;
                _contentProperty.GetArrayElementAtIndex(_contentProperty.arraySize - 1).FindPropertyRelative(KeyName)
                    .intValue = index;
                _contentProperty.serializedObject.ApplyModifiedProperties();
            }, name);
        }
    }
}