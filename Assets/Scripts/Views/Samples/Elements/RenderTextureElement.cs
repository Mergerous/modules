using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class RenderTextureElement : Element
    {
        [SerializeField] private RenderTexture texture;

        public RenderTexture Texture => texture;
    }
}
