using System;
using Modules.States;
using VContainer;
using VContainer.Unity;

namespace States
{
    [Serializable]
    public sealed class StatesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StatesEntryPoint>();
            builder.Register<StatesManager>(Lifetime.Singleton);
        }
    }
}