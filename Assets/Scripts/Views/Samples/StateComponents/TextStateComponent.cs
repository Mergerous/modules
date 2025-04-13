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
