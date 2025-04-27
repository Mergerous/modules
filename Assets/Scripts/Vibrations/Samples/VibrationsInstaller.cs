using System;
using Modules.Vibrations;
using VContainer;
using VContainer.Unity;

namespace Vibrations.Samples
{
    [Serializable]
    public sealed class VibrationsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<VibrationsManager>(Lifetime.Singleton);
        }
    }
}