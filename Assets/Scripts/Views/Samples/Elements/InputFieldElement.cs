using System;
using R3;
using TMPro;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class InputFieldElement : Element
    {
        private readonly Subject<string> onSubmitSubject = new();
        
        [SerializeField] private TMP_InputField inputField;

        public Observable<string> OnSubmitObservable => onSubmitSubject;

        public string Text => inputField.text;

        public void SetText(string text)
        {
            inputField.text = text;
        }

        public override void Initialize()
        {
            base.Initialize();
            inputField.onEndEdit.AddListener(OnSubmit);
        }

        public override void Dispose()
        {
            base.Dispose();
            inputField.onEndEdit.RemoveListener(OnSubmit);
        }
        
        private void OnSubmit(string text)
        {
            onSubmitSubject.OnNext(text);
        }
    }
}
