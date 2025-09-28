using System;
using Data.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Modules.Data
{
    [Serializable]
    public sealed class DataInstaller : IInstaller
    {
        [SerializeField] private DataSettings dataSettings;
        
        public void Install(IContainerBuilder builder)
        {
            builder
                .Register<IDataService, PlayerPrefsDataManager>(Lifetime.Singleton)
                .Keyed(DataConstants.DATA_PLAYER_PREFS_KEY)
                .WithParameter(dataSettings);
            
            builder
                .Register<IDataService, PersistentDataManager>(Lifetime.Singleton)
                .Keyed(DataConstants.DATA_PERSISTENS_DATA_KEY)
                .WithParameter(dataSettings);
            
            builder.Register<DataManager>(Lifetime.Singleton).WithParameter(dataSettings);
        }
    }
}
