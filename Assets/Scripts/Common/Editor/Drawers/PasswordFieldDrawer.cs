using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Modules.Common
{
    [CustomPropertyDrawer(typeof(PasswordFieldAttribute))]
    public class PasswordFieldDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            TextField passwordField = new TextField(property.displayName, 50, false, true, '*');
            passwordField.BindProperty(property);
            return passwordField;
        }
    }
}
