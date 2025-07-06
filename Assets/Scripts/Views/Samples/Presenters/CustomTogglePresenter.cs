using JetBrains.Annotations;
using Lofelt.NiceVibrations;
using Modules.Audio;
using Modules.Vibrations;
using R3;
using UnityEngine.UI;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class CustomTogglePresenter : Presenter
    {
        private readonly AudioManager audioManager;
        private readonly VibrationsManager vibrationsManager;
        private readonly ReactiveProperty<bool> value = new();
        
        private ToggleElement toggleElement;
        public Observable<bool> ValueChangedObservable => value;
        public bool Value => value.Value;

        public CustomTogglePresenter(AudioManager audioManager, VibrationsManager vibrationsManager)
        {
            this.audioManager = audioManager;
            this.vibrationsManager = vibrationsManager;
        }
        
        public void Subscribe(View view, View rootView = null)
        {
            base.Subscribe();

            toggleElement = view.GetElement<ToggleElement>("toggle");
            SoundElement soundElement = view.GetElement<SoundElement>("sound");
            VibrationElement vibrationElement = view.GetElement<VibrationElement>("vibration");
            
            if (rootView != null)
            {
                ToggleGroup group = rootView.GetElement<ToggleGroupElement>("toggle_group").Group;
                toggleElement.SetToggleGroup(group);
            }
            
            Observable<bool> observable = toggleElement.IsOnObservable
                .Do(isOn =>
                {
                    view.SetState(isOn ? CustomToggleState.On : CustomToggleState.Off);
                    value.Value = isOn;
                });
                
            observable
                .Where(_ => soundElement?.CanPlay ?? false)
                .Subscribe(_ => audioManager.PlaySound(soundElement.SoundKey))
                .AddTo(disposables);

            observable
                .Where(_ => vibrationElement?.CanPlay ?? false)
                .Subscribe(_ => vibrationsManager.Play(vibrationElement.HapticType))
                .AddTo(disposables);
        }

        public void SetValue(bool isOn)
        {
            toggleElement.SetValue(isOn);
        }
    }
}
