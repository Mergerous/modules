using System;
using Modules.Debugging;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Modules.Debugging
{
    [Serializable]
    public sealed class DebugInstaller : IInstaller
    {
        [SerializeField] private DebugContainer debugContainer;
        
        public void Install(IContainerBuilder builder)
        {
            builder.Register<DebugManager>(Lifetime.Singleton).WithParameter(debugContainer);
            builder.RegisterEntryPoint<DebugEntryPoint>();
        }
    }
}
