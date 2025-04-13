using System;
using System.Reflection;
using Modules.Remote.Attributes;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Remote.Editor
{
    #if ODIN_INSPECTOR
    
    public class JsonWizard : OdinEditorWindow
    {
        private object target;
        private Object source;
        
        public static void Show(object target, Object source)
        {
            JsonWizard wnd = CreateInstance<JsonWizard>();
            wnd.target = target;
            wnd.ShowAuxWindow();
        }

        protected override object GetTarget()
        {
            return target;
        }
    }
    
    #else
  
    public class JsonWizard : EditorWindow
    {
        private SerializedProperty target;

        public static void Show(SerializedProperty target)
        {
            JsonWizard wnd = CreateInstance<JsonWizard>();
            wnd.target = target;
            wnd.ShowAuxWindow();
        }
        
        private void CreateGUI()
        {
            PropertyField targetField = new PropertyField();
            targetField.BindProperty(target);
            targetField.label = target.managedReferenceValue.GetType().GetCustomAttribute<AddToRemoteAttribute>().searchName;
            rootVisualElement.Add(targetField);
        }
    }

    #endif
}
