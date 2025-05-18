using System;
using TMPro;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public class TextElement : Element
    {
        [SerializeField] private TMP_Text text;

        public void SetText(string text)
        {
            this.text.SetText(text);
        }

        public void SetArguments(params object[] arguments)
        {
            text.text = string.Format(text.text, arguments);
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
