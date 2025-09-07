using System.Linq;
using Data.Runtime;
using JetBrains.Annotations;
using UnityEngine;

namespace Modules.Data
{
    [UsedImplicitly]
    internal sealed class PlayerPrefsDataManager : IDataService
    {
        private readonly DataSettings dataSettings;

        public PlayerPrefsDataManager(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;
        }

        void IDataService.Save(string key, object data)
        {
            string json = Serializer.Serialize(data, dataSettings.SerializationSettings);
            PlayerPrefs.SetString(key, json);
        }

        T IDataService.LoadOrDefault<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                object[] dump = Serializer.Deserialize<object[]>(dataSettings.DefaultDumpAsset.text, dataSettings.SerializationSettings);
                return dump.OfType<T>().FirstOrDefault();
            }

            string json = PlayerPrefs.GetString(key);
            return Serializer.Deserialize<T>(json, dataSettings.SerializationSettings);
        }
    }
}