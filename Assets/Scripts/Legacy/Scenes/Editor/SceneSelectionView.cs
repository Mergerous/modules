using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Modules.Scenes.Editor
{
    public class SceneSelectionView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<SceneSelectionView, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
            }
        }

        public SceneSelectionView()
        {
            ObjectField objectField = new ObjectField();
            objectField.style.flexGrow = 1f;
            objectField.objectType = typeof(SceneAsset);
            Add(objectField);
        }
    }
}
