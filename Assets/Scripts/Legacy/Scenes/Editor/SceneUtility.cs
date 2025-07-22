using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Object = UnityEngine.Object;


public class SceneUtility : EditorWindow
{
    private const string DefaultFolderPath = @"Assets/Scenes";
    public static bool SaveScenesAutomatically = true;
    public List<SceneSelection> _scenes = new List<SceneSelection>();
    Vector2 scrollPos;
    [SerializeField] private Object Folder;
    
    [MenuItem("Window/Scene Utility")]
    static void Init()
    {
        SceneUtility window = (SceneUtility)GetWindow(typeof(SceneUtility));
        window.Show();
        window.Repaint();
    }

    private void OnEnable()
    {
        if (!Folder)
        {
            Folder = AssetDatabase.LoadAssetAtPath<Object>(DefaultFolderPath);
        }
    }
    

    void OnGUI()
    {
        titleContent = new GUIContent("Scene Utility", EditorGUIUtility.FindTexture("d_BuildSettings.SelectedIcon"));
        if(EditorApplication.isPlaying) return;
       
        EditorGUILayout.BeginHorizontal();
        SaveScenesAutomatically = GUILayout.Toggle(SaveScenesAutomatically, EditorGUIUtility.IconContent(SaveScenesAutomatically? "SaveActive": "SavePassive"), EditorStyles.miniButton,GUILayout.Width(40));
        if (GUILayout.Button("Load", EditorStyles.miniButtonLeft))
        {
            var path = AssetDatabase.GetAssetPath(Folder);
            var info = new DirectoryInfo(path);
            foreach (var file in  info.GetFiles())
            {
                var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path + "/" + file.Name);
                if (scene != null)
                {
                    _scenes.Add(new SceneSelection()
                    {
                        p1 = scene
                    });
                }
            }
        }
        if (GUILayout.Button("Clear",  EditorStyles.miniButtonRight))
        {
            _scenes.Clear();
        }

        Folder = EditorGUILayout.ObjectField(Folder, typeof(Object));
        EditorGUILayout.EndHorizontal();
        
        scrollPos = EditorGUILayout.BeginScrollView (scrollPos,
            false,
            false);

        _scenes.RemoveAll(selection => selection.ReadyToRemove);

        SerializedObject so = new SerializedObject(this);
        SerializedProperty prop = so.FindProperty(nameof(_scenes));
        EditorGUILayout.PropertyField(prop, true);

        so.ApplyModifiedProperties();
        EditorGUILayout.EndScrollView(); 
        EditorGUILayout.Space(10);
    }

    public static void SaveScene(Object scene)
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetSceneByPath(AssetDatabase.GetAssetPath(scene)));
    }

    public static void SaveAllScenes()
    {
        EditorSceneManager.SaveOpenScenes();
    }
    public static bool IsOpenScene(Object scene)
    {
        return EditorSceneManager.GetSceneByPath(AssetDatabase.GetAssetPath(scene)).isLoaded;
    }
    
    public static bool IsActiveScene(Object scene)
    {
        return EditorSceneManager.GetActiveScene() ==  EditorSceneManager.GetSceneByPath(AssetDatabase.GetAssetPath(scene));
    }
    
    public static void OpenScene(Object scene, OpenSceneMode mode = OpenSceneMode.Single)
    {
        EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(scene), mode);
    }

    public static void RemoveScene(Object scene)
    {
        var instance = EditorSceneManager.GetSceneByPath(AssetDatabase.GetAssetPath(scene));
        EditorSceneManager.CloseScene(instance, true);
    }
    public static void LaunchScene()
    {
        EditorApplication.EnterPlaymode();
    }

}

[Serializable]
public class SceneSelection
{
    public bool ReadyToRemove;
    public bool IsSelected;
    public bool IsAdded;
    public SceneAsset p1;
}