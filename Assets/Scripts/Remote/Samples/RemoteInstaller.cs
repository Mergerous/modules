using System;
using VContainer;
using VContainer.Unity;

namespace Modules.Remote
{
    [Serializable]
    public sealed class RemoteInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AssetManager>().AsSelf();
        }
    }
}