using JetBrains.Annotations;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Views
{
    [UsedImplicitly]
    public sealed class ToggleElement : Element
    {
        private readonly ReactiveProperty<bool> isOn = new ReactiveProperty<bool>();

        [SerializeField] private Toggle toggle;

        public Observable<bool> IsOnObservable => isOn;

        public bool IsOn => isOn.Value;

        public void SetValue(bool isOn)
        {
            toggle.isOn = isOn;
        }

        public override void Initialize()
        {
            base.Initialize();
            toggle.onValueChanged.AddListener(OnValueChanged);
        }

        public override void Dispose()
        {
            base.Dispose();
            toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            this.isOn.Value = isOn;
        }
    }
}
