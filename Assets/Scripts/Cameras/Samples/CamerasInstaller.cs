using System;
using Modules.Cameras;
using VContainer;
using VContainer.Unity;

namespace Cameras.Samples
{
    [Serializable]
    public sealed class CamerasInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<CameraManager>(Lifetime.Singleton);
        }
    }
}
