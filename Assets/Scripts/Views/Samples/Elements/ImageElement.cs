using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
    [Serializable]
    public class ImageElement : Element
    {
        [SerializeField] private Image image;

        public void SetSprite(Sprite sprite) => image.sprite = sprite;

        public void SetFillAmount(float amount) => image.fillAmount = amount;
        
        public override void Initialize()
        {
            
        }

        public override void Dispose()
        {
            
        }
    }
}
