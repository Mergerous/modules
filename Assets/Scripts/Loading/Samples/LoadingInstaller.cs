using System;
using Modules.States;
using VContainer;
using VContainer.Unity;

namespace Loading
{
    [Serializable]
    public sealed class LoadingInstaller : IInstaller
    {
        void IInstaller.Install(IContainerBuilder builder)
        {
            // States
            //
            builder.Register<IState, LoadingState>(Lifetime.Singleton);
            
            // Core
            //
            builder.Register<LoadingManager>(Lifetime.Singleton);
            
            // Models
            //
            builder.Register<ILoadingProgress, LoadingModel>(Lifetime.Singleton);

            // Views
            //
            builder.RegisterEntryPoint<LoadingPresenter>().AsSelf();
        }
    }
}