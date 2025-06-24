using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class AnchorElement : Element
    {
        public string anchorKey;
        public Transform anchor;
    }
}
