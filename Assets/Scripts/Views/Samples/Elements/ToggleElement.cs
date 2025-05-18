using JetBrains.Annotations;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class ToggleElement : Element
    {
        private readonly Subject<bool> isOnSubject = new();

        [SerializeField] private Toggle toggle;

        public Observable<bool> IsOnObservable => isOnSubject;

        public bool IsOn => toggle.isOn;

        public void SetValue(bool isOn, bool shouldNotify = true)
        {
            if (shouldNotify)
            {
                toggle.isOn = isOn;
            }
            else
            {
                toggle.SetIsOnWithoutNotify(isOn);
            }
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
            isOnSubject.OnNext(IsOn);
        }
    }
}
