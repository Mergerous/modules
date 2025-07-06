using UnityEngine;

namespace Modules.Views
{
    public interface ISpriteContent
    {
        public string Url { get; }
        public Sprite Sprite { get; set; }
    }
}