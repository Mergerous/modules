using System.IO;
using System.Linq;
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

        async void IDataService.SaveAsync(string key, object data, CancellationToken cancellationToken)
        {
            string json = Serializer.Serialize(data, dataSettings.SerializationSettings);
            string path = Path.Combine(Application.persistentDataPath, key);
            await File.WriteAllTextAsync(path, json, cancellationToken);
        }

        // TODO ADD Handle
        async Task<T> IDataService.LoadOrDefaultAsync<T>(string key, CancellationToken cancellationToken)
        {
            string path = Path.Combine(Application.persistentDataPath, key);
            
            if (!File.Exists(path))
            {
                return dataSettings.DefaultDumpAsset.GetDataOrDefault<T>();
            }
            
            string json = await File.ReadAllTextAsync(path, cancellationToken);
            return Serializer.Deserialize<T>(json, dataSettings.SerializationSettings);
        }
    }
}