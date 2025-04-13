using System;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
    [UsedImplicitly, Serializable]
    public sealed class ButtonElement : Element
    {
        private readonly Subject<Unit> clickSubject = new Subject<Unit>();

        public Observable<Unit> ClickObservable => clickSubject;

        [SerializeField] private Button button;

        public override void Initialize()
        {
            base.Initialize();
            button.onClick.AddListener(OnClick);
        }

        public override void Dispose()
        {
            base.Dispose();
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            clickSubject.OnNext(Unit.Default);
        }
    }
}
