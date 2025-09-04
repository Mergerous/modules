using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Modules.Data;
using UnityEngine;

namespace Data.Runtime
{
    [UsedImplicitly]
    internal sealed class PersistentDataManager : IDataService
    {
        private readonly DataSettings dataSettings;

        public PersistentDataManager(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;
        }

        public async void SaveAsync(string key, object data, CancellationToken cancellationToken)
        {
            string json = Serializer.Serialize(data, dataSettings.JsonType, dataSettings.EncodingType);
            await File.WriteAllTextAsync(Application.persistentDataPath, json, cancellationToken);
        }

        public async Task<T> LoadOrFallbackAsync<T>(string key, T fallback, CancellationToken cancellationToken)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return fallback;
            }
            
            string json = await File.ReadAllTextAsync(Application.persistentDataPath, cancellationToken);
            return Serializer.Deserialize<T>(json, dataSettings.JsonType, dataSettings.EncodingType);
        }
    }
}