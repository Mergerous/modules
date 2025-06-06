using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Modules.Common.Editor
{
    public abstract class SettingsEditor : UnityEditor.Editor
    {
        protected abstract string ManagedReference { get; }
        
        protected abstract Type TargetType { get; }
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();

            SerializedProperty array = serializedObject.FindProperty(ManagedReference);
            
            PolyList listView = new PolyList();
            listView.OnItemAdded += (index, type) =>
            {
                array.InsertArrayElementAtIndex(index);
      
                SerializedProperty prop = array.GetArrayElementAtIndex(index);
                prop.managedReferenceValue = Activator.CreateInstance(type);
                
                PropertyField field = new PropertyField();
                field.BindProperty(prop);
                
                serializedObject.ApplyModifiedProperties();
                return field;
            };

            listView.OnItemRemoved += index =>
            {
                array.DeleteArrayElementAtIndex(index);
                serializedObject.ApplyModifiedProperties();
            };

            listView.SetMenu(TargetType);
            
            for (int i = 0; i < array.arraySize; i++)
            {
                var field = new PropertyField(array.GetArrayElementAtIndex(i));
                listView.AddItem(field);
            }
            
            inspector.Add(listView);

            return inspector;
        }
    }
}