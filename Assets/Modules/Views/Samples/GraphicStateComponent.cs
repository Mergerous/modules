using System;
using Modules.UI.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Views
{
    [Flags]
    public enum GraphicStateOptions
    {
        None  = 0,
        Color = 1 << 0,
    }

    [Serializable]
    public abstract class GraphicStateComponent<T> : BehaviourStateComponent<T> where T : Graphic
    {
        [SerializeField] private GraphicStateOptions graphicStateOptions;
        [SerializeField, ShowIf(nameof(ChangeColor))] private Color color;

        private bool ChangeColor => graphicStateOptions.HasFlag(GraphicStateOptions.Color);
        
        public override void Apply()
        {
            base.Apply();
            if (ChangeColor)
            {
                subject.color = color;
            }
        }
    }
}