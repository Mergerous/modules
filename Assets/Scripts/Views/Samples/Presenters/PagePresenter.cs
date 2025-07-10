using JetBrains.Annotations;

namespace Modules.Views
{
    public enum PageState
    {
        On,
        Off
    }
    
    [UsedImplicitly]
    public abstract class PagePresenter : Presenter
    {
        private View view;
        
        public virtual void Subscribe(View view)
        {
            base.Subscribe();
            this.view = view;
        }

        public void Select(bool isSelected)
        {
            view.SetState(isSelected ? PageState.On : PageState.Off);
        }
    }
}