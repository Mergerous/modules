using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PolyList : VisualElement
{
    private readonly VisualElement _container;
    private readonly VisualElement _buttonsPanel;

    private ToolbarMenu _addButton;
    private ToolbarButton _removeButton;
    private GenericMenu _contextualMenu;

    public event Func<int, Type, VisualElement> OnItemAdded;
    public event Action<int> OnItemRemoved;

    public PolyList()
    {

        _container = new VisualElement();
        _buttonsPanel = new VisualElement();
        _buttonsPanel.style.flexDirection = FlexDirection.RowReverse;
        
        _addButton = new ToolbarMenu();
        _addButton.RegisterCallback<ClickEvent>(OnAddClicked);
        _addButton.text = "+";
        _addButton.style.fontSize = 20;

        _removeButton = new ToolbarButton();
        _removeButton.text = "-";
        _removeButton.RegisterCallback<ClickEvent>(OnRemoveClicked);
        _removeButton.style.fontSize = 20;

        _buttonsPanel.Add(_removeButton);
        _buttonsPanel.Add(_addButton);
        
        Add(_container);
        Add(_buttonsPanel);
    }


    private void OnAddClicked(ClickEvent clickEvent)
    {
        _contextualMenu.ShowAsContext();
    }

    public void SetMenu(Type type)
    {
        _contextualMenu = new GenericMenu();

        foreach (var tt in  Assembly.GetAssembly(type).GetTypes().Where(t => t.IsSubclassOf(type)))
        {
            _contextualMenu.AddItem(new GUIContent(tt.Name), false, () =>
            {
                var item = OnItemAdded?.Invoke(_container.childCount, tt);
                AddItem(item);
            });
        }
    }

    private void OnRemoveClicked(ClickEvent clickEvent)
    {
        if (_container.childCount > 0)
        {
            OnItemRemoved?.Invoke(_container.childCount - 1);
            _container.RemoveAt(_container.childCount - 1);
        }
    }
    
    public void AddItem(VisualElement element)
    {
        Box externBox = new Box();
        Box internBox = new Box();

        internBox.style.marginBottom = internBox.style.marginRight = internBox.style.marginTop = 10;
        internBox.style.marginLeft = 15;
        
        internBox.style.paddingBottom = internBox.style.paddingRight = internBox.style.paddingTop = 10;
        
        internBox.style.borderBottomWidth =
            internBox.style.borderTopWidth = internBox.style.borderLeftWidth = internBox.style.borderRightWidth = 1f;
        
        internBox.style.borderBottomColor = internBox.style.borderTopColor =
            internBox.style.borderLeftColor = internBox.style.borderRightColor = Color.gray;
        
        internBox.Add(element);
        externBox.Add(internBox);
        _container.Add(externBox);
    }
}
