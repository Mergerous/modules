using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Views
{
    [Serializable]
    public sealed class SliderElement : Element
    {
        [SerializeField] private Slider slider;

        public void SetValue(float value)
        {
            slider.value = value;
        }

        public void SetMaxValue(float maxValue)
        {
            slider.maxValue = maxValue;
        }
    }
}
