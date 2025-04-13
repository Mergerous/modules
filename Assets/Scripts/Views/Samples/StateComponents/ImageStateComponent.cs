using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
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
