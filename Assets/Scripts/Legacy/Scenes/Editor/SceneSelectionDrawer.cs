using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneSelection))]
public class SceneSelectionDrawer : PropertyDrawer
{
    private const int _offset = 10;
    private const int _space = 1;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        bool isAdded = SceneUtility.IsOpenScene(property.FindPropertyRelative("p1").objectReferenceValue);
        bool isActive = SceneUtility.IsActiveScene(property.FindPropertyRelative("p1").objectReferenceValue);
        
        var index = 0;
        var width = position.width / 9f;
        var startPos = position.x - _offset;

        var rect1 = new Rect(startPos, position.y, width, position.height);
        var rect3 = new Rect(startPos + (width + _space) * ++index, position.y, width, position.height);
        var rect4 = new Rect(startPos + (width + _space) * ++index, position.y, width, position.height);
        var rect5 = new Rect(startPos + (width + _space) * ++index, position.y, width, position.height);
        var rect2 = new Rect(startPos + (width + _space) * ++index, position.y,
            position.width - (width + _space) * index + _offset, position.height);
        
        isActive = GUI.Toggle(rect1, isActive, EditorGUIUtility.IconContent("d_editicon.sml"),
            new GUIStyle(EditorStyles.miniButtonLeft));
        
        if (isActive != property.FindPropertyRelative("IsSelected").boolValue)
        {
            if (isActive)
            {
                SceneUtility.OpenScene(property.FindPropertyRelative("p1").objectReferenceValue);
            }
            property.FindPropertyRelative("IsSelected").boolValue = isActive;
        }

        if (GUI.Button(rect4, EditorGUIUtility.IconContent("TreeEditor.Trash", "Remove from list"), EditorStyles.miniButtonMid))
        {
            property.FindPropertyRelative("ReadyToRemove").boolValue = true;
        }

        GUI.enabled = !isActive;

        isAdded = GUI.Toggle(rect5, isAdded && !isActive,EditorGUIUtility.IconContent( isAdded && !isActive? "d_ol_minus_act@2x": "d_ol_plus_act@2x"),
                                                             new GUIStyle(EditorStyles.miniButtonRight));
        if (isAdded != property.FindPropertyRelative("IsAdded").boolValue)
        {
            property.FindPropertyRelative("IsAdded").boolValue = isAdded;
            if (isAdded)
            {
                SceneUtility.OpenScene(property.FindPropertyRelative("p1").objectReferenceValue,
                    OpenSceneMode.Additive);
            }
            else
            {
                if (SceneUtility.SaveScenesAutomatically)
                {
                    SceneUtility.SaveScene(property.FindPropertyRelative("p1").objectReferenceValue);
                }
                SceneUtility.RemoveScene(property.FindPropertyRelative("p1").objectReferenceValue);
            }
        }


        //property.FindPropertyRelative("IsAdded").boolValue = false;
        

        GUI.enabled = true;

        if (GUI.Button(rect3, EditorGUIUtility.IconContent("PlayButton"), EditorStyles.miniButtonMid))
        {
            if (SceneUtility.SaveScenesAutomatically)
            {
                SceneUtility.SaveScene(property.FindPropertyRelative("p1").objectReferenceValue);
            }
            SceneUtility.OpenScene(property.FindPropertyRelative("p1").objectReferenceValue);
            SceneUtility.LaunchScene();
        }
        
        EditorGUI.PropertyField(rect2, property.FindPropertyRelative("p1"), GUIContent.none);
        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();
        
    }
}
