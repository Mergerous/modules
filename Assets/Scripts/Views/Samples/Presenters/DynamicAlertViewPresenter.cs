using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class DynamicAlertViewPresenter
    {
        private const string TEXT_KEY = "text";

        private View view;

        public DynamicAlertViewPresenter(View view)
        {
            this.view = view;
        }

        public void SetText(string text)
        {
            view.GetElement<TextElement>(TEXT_KEY).SetText(text);
        }

        public Tweener DoMoveUp(Transform source)
        {
            view.transform.SetParent(source, false);
            view.transform.position = source.position;
            return view.transform.DOLocalMoveY(100f, 1f);
        }
    }
}
