using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Scenes.Editor
{
    public class MiniToggleView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<MiniToggleView, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlBoolAttributeDescription value = new UxmlBoolAttributeDescription { name = nameof(Value) };

            UxmlStringAttributeDescription image = new UxmlStringAttributeDescription() { name = nameof(image)};
            
            UxmlStringAttributeDescription image2 = new UxmlStringAttributeDescription() { name = nameof(image2) };
            
            UxmlColorAttributeDescription color = new UxmlColorAttributeDescription() { name = nameof(color), defaultValue = Color.clear};
            
            UxmlColorAttributeDescription color2 = new UxmlColorAttributeDescription() { name = nameof(color2),  defaultValue = Color.clear };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                MiniToggleView ate = ve as MiniToggleView;
                
                ate.Clear();
                
                VisualElement element1 = new VisualElement
                {
                    name = "enabled",
                    style =
                    {
                        flexGrow = 1f,
                        unityBackgroundScaleMode = ScaleMode.ScaleToFit
                    }
                };

                ve.Add(element1);

                ate.image = image.GetValueFromBag(bag, cc);
                ate.image2 = image2.GetValueFromBag(bag, cc);
                ate.color = color.GetValueFromBag(bag, cc);
                ate.color2 = color2.GetValueFromBag(bag, cc);
                ate.Value = value.GetValueFromBag(bag, cc);
            }
        }

        private bool value;
        private string image { get; set; }
        private string image2 { get; set; }

        private Color color { get; set; }
        private Color color2 { get; set; }


        public bool Value
        {
            get => value;
            set
            {
                this.value = value;
                style.backgroundColor = value ? color : color2;
                this.Q<VisualElement>("enabled").style.backgroundImage = value
                    ? new StyleBackground(EditorGUIUtility.FindTexture(image))
                    : new StyleBackground(EditorGUIUtility.FindTexture(image2));
            }
        }

        private void OnClicked(ClickEvent evt)
        {
            Value = !Value;
        }
        
        public MiniToggleView()
        {
            RegisterCallback<ClickEvent>(OnClicked);
        }
        
        
        
        ~MiniToggleView()
        {
            UnregisterCallback<ClickEvent>(OnClicked);
        }
    }
}