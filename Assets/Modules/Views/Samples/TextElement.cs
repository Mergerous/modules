using System;
using TMPro;
using UnityEngine;

namespace Modules.UI.Views
{
    [Serializable]
    public class TextElement : Element
    {
        [SerializeField] private TMP_Text text;

        public void SetText(string text)
        {
            this.text.SetText(text);
        }

        public void SetColor(Color color)
        {
            text.color = color;
        }
        
        public override void Initialize()
        {

        }

        public override void Dispose()
        {

        }
    }
}
