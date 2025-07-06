using JetBrains.Annotations;
using Lofelt.NiceVibrations;
using Modules.Audio;
using Modules.Vibrations;
using R3;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class ButtonPresenter : Presenter
    {
        private readonly Subject<Unit> clickSubject = new();
        private readonly AudioManager audioManager;
        private readonly VibrationsManager vibrationsManager;
        
        public Observable<Unit> ClickObservable => clickSubject;

        public ButtonPresenter(AudioManager audioManager, VibrationsManager vibrationsManager)
        {
            this.audioManager = audioManager;
            this.vibrationsManager = vibrationsManager;
        }

        public void Subscribe(View view)
        {
            base.Subscribe();
            SoundElement soundElement = view.GetElement<SoundElement>("sound");
            
            view.GetElement<ButtonElement>("button").ClickObservable
                .Do(_ => clickSubject.OnNext(Unit.Default))
                .Where(_ => soundElement?.CanPlay ?? false)
                .Subscribe(_ =>
                {
                    audioManager.PlaySound(soundElement.SoundKey);
                    // vibrationsManager.Play(HapticPatterns.PresetType.SoftImpact);
                })
                .AddTo(disposables);
        }
    }
}