using System;
using System.Collections.Generic;
using Modules.Scenes.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public class ScenesWindow : EditorWindow
{
    [MenuItem("Window/UI Toolkit/ScenesWindow")]
    public static void ShowExample()
    {
        ScenesWindow wnd = GetWindow<ScenesWindow>();
        wnd.titleContent = new GUIContent("ScenesWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Modules/Scenes/Editor/Window/ScenesUtility.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Modules/Scenes/Editor/Window/ScenesUtility.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        root.Add(labelWithStyle);
        
        
        const int itemCount = 1;
        var items = new List<string>(itemCount);
        for (int i = 1; i <= itemCount; i++)
            items.Add(i.ToString());
        
        Func<VisualElement> makeItem = () =>
        {
            return new SceneSelectionView();
        };

        Action<VisualElement, int> bindItem = (e, i) => { };
        
        const int itemHeight = 22;
        
        ListView listView = new ListView(items, itemHeight, makeItem, bindItem)
        {
            selectionType = SelectionType.Multiple
        };
        
        listView.showAddRemoveFooter = true;
        listView.Q<Button>("unity-list-view__add-button").clickable = new Clickable(() =>
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Single"), false, o =>
            {
                items.Add("SSS");
                listView.RefreshItems();
            }, null);
            menu.AddItem(new GUIContent("Multiple"), false, o =>
            {
                items.Add("SSS");
                listView.RefreshItems();
            }, null);
            menu.ShowAsContext();
        });

        listView.style.flexShrink = 0f;
        listView.style.flexGrow = 0f;
        listView.showBorder = true;
        listView.reorderMode = ListViewReorderMode.Animated;
        listView.onSelectionChange += objects => Debug.Log($"Selected: {string.Join(", ", objects)}");
        listView.onItemsChosen += objects => Debug.Log($"Double-clicked: {string.Join(", ", objects)}");
        listView.style.flexGrow = 1.0f;

        root.Add(listView);
    }
}