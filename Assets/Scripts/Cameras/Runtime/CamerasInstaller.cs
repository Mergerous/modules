using System;
using Modules.Cameras;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Cameras.Samples
{
    [Serializable]
    public sealed class CamerasInstaller : IInstaller
    {
        [SerializeField] private CamerasContainer camerasContainer;
        public void Install(IContainerBuilder builder)
        {
            builder.Register<CameraManager>(Lifetime.Singleton).WithParameter(camerasContainer);
        }
    }
}
