using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
    [Serializable]
    public sealed class HorizontalLayoutElement : Element
    {
        [SerializeField] private HorizontalLayoutGroup group;

        public HorizontalLayoutGroup Group => group;
    }
}