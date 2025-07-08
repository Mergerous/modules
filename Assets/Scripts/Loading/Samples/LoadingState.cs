using JetBrains.Annotations;
using Modules.States;

namespace Loading
{
    [UsedImplicitly]
    public sealed class LoadingState : IState<LoadingPayload>
    {
        private readonly LoadingPresenter presenter;
        
        public LoadingPayload Payload { private get; set; }

        public LoadingState(LoadingPresenter presenter)
        {
            this.presenter = presenter;
        }

        void IState.Open()
        {
            presenter.Subscribe(Payload);
        }
        
        void IState.Close()
        {
            presenter.Unsubscribe();
        }
    }
}