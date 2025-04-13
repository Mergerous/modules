using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Common.Views
{
    [Flags]
    public enum TextStateOptions
    {
        None  = 0,
        Text = 1 << 0,
    }
    
    [Serializable]
    public sealed class TextStateComponent : GraphicStateComponent<TextMeshProUGUI>
    {
        [SerializeField] private TextStateOptions textStateOptions;
        [SerializeField, ShowIf(nameof(ChangeText))] private string text;

        private bool ChangeText => textStateOptions.HasFlag(TextStateOptions.Text);

        public override void Apply()
        {
            if (ChangeText)
            {
                subject.SetText(text);
            }
        }
    }
}
