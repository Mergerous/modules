using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class TextStateComponent : GraphicStateComponent<TextMeshProUGUI>
    {
        [SerializeField] private TextStateOptions textStateOptions;
        [SerializeField, ShowIf(nameof(ChangeText))] private string text;
        [SerializeField, ShowIf(nameof(ChangeStyle))] private FontStyles style;
        [SerializeField, ShowIf(nameof(ChangeOutlineColor))] private Color outlineColor;
        
        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
        private bool ChangeText => textStateOptions.HasFlag(TextStateOptions.Text);
        private bool ChangeStyle => textStateOptions.HasFlag(TextStateOptions.Style);
        private bool ChangeOutlineColor => textStateOptions.HasFlag(TextStateOptions.OutlineColor);

        public override void Apply()
        {
            base.Apply();
            if (ChangeText)
            {
                subject.SetText(text);
            }

            if (ChangeStyle)
            {
                subject.fontStyle = style;
            }

            if (ChangeOutlineColor)
            {
                subject.fontMaterial.SetColor(OutlineColor, outlineColor);
            }
        }
    }
}
