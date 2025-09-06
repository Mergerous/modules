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

        public void Save(string key, object data)
        {
            string json = Serializer.Serialize(data, dataSettings.SerializationSettings);
            PlayerPrefs.SetString(key, json);
        }

        public T LoadOrFallback<T>(string key, T fallback)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return fallback;
            }

            string json = PlayerPrefs.GetString(key);
            return Serializer.Deserialize<T>(json, dataSettings.SerializationSettings);
        }
    }
}