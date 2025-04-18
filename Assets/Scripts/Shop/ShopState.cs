using JetBrains.Annotations;
using Modules.States;

namespace Shop
{
    [UsedImplicitly]
    public sealed class ShopState : IState
    {
        private readonly ShopPresenter presenter;

        public ShopState(ShopPresenter presenter)
        {
            this.presenter = presenter;
        }
        
        public void Open()
        {
            presenter.Subscribe();
        }

        public void Close()
        {
            presenter.Unsubscribe();
        }
    }
}