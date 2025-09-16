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
            switch (dataSettings.DataType)
            {
                case DataType.PersistentData:
                    builder
                        .Register<IDataService, PlayerPrefsDataManager>(Lifetime.Singleton)
                        .Keyed(DataConstants.PLAYER_PREFS_KEY)
                        .WithParameter(dataSettings);
                    break;
                case DataType.PlayerPrefs:
                    builder
                        .Register<IDataService, PersistentDataManager>(Lifetime.Singleton)
                        .Keyed(DataConstants.PERSISTENS_DATA_KEY)
                        .WithParameter(dataSettings);
                    break;
                default:
                    builder.Register<DataManager>(Lifetime.Singleton).WithParameter(dataSettings);
                    break;
            }
        }
    }
}
