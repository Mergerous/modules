using System;
using Modules.Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Modules
{
    [Serializable]
    public sealed class DataInstaller : IInstaller
    {
        [SerializeField] private DataSettings dataSettings;
        
        public void Install(IContainerBuilder builder)
        {
            builder.Register<DataManager>(Lifetime.Singleton).WithParameter(dataSettings);
        }
    }
}
