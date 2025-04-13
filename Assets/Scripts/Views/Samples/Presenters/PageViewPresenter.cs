using JetBrains.Annotations;
using Modules.UI.Views;

namespace Shop.Views
{
    public enum PageState
    {
        On,
        Off
    }
    
    [UsedImplicitly]
    public abstract class PageViewPresenter
    {
        private View view;

        public void Initialize(View view)
        {
            this.view = view;
        }
        
        public void Select(bool isSelected)
        {
            view.SetState(isSelected ? PageState.On : PageState.Off);
        }
    }
}