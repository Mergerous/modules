using System;
using Common.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Views.States
{
    [Flags]
    public enum ImageStateOptions
    {
        None   = 0,
        Sprite = 1 << 0,
    }
    
    [Serializable]
    public sealed class ImageStateComponent : GraphicStateComponent<Image>
    {
        [SerializeField] private ImageStateOptions imageStateOptions;
        [SerializeField, ShowIf(nameof(ChangeSprite))] private Sprite sprite;

        public bool ChangeSprite => imageStateOptions.HasFlag(ImageStateOptions.Sprite);
        
        public override void Apply()
        {
            base.Apply();
            if (ChangeSprite)
            {
                subject.sprite = sprite;
            }
        }
    }
}
