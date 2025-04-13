using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
// using Firebase.RemoteConfig;
using JetBrains.Annotations;
using UnityEngine;

namespace Modules.Remote
{
    [UsedImplicitly]
    public sealed class RemoteManager
    {
        private readonly JsonLibrary jsonLibrary;
        private readonly RemoteSettings remoteSettings;
        // private readonly FirebaseRemoteConfig remoteConfig;

        public RemoteManager(JsonLibrary jsonLibrary, RemoteSettings remoteSettings, params IRemoteHandler[] remotables)
        {
            this.remoteSettings = remoteSettings;
            this.jsonLibrary = jsonLibrary;
            // remoteConfig = FirebaseRemoteConfig.DefaultInstance;

            foreach (IRemoteHandler remotable in remotables)
            {
                GetFromRemoteAttribute attribute = remotable.GetType().GetCustomAttribute<GetFromRemoteAttribute>();
                if (attribute != null && TryFetch(attribute.Type, out object remote))
                {
                    remotable.OnFetched(new RemoteInfoHandle(remote));
                }
            }
        }

        public async Task LoadAsync()
        {
            Dictionary<string, object> defaults = jsonLibrary.Defaults
                .ToDictionary(
                    remote => remote.GetType().GetCustomAttribute<AddToRemoteAttribute>().searchName, 
                    remote => (object)JsonUtility.ToJson(remote));
            
            // await remoteConfig.SetDefaultsAsync(defaults);
            // if (remoteSettings.ShouldFetchOnStart)
            // {
            //     await remoteConfig.FetchAsync(TimeSpan.Zero);
            // }
            //
            // await remoteConfig.ActivateAsync();
        }

        public bool TryFetch<T>(out T value) where T : class
        {
            if (TryFetch(typeof(T), out object result))
            {
                value = result as T;
                return value != null;
            }
            
            value = default;
            return false;
        }
        
        public bool TryFetch(Type type, out object value)
        {
            // AddToRemoteAttribute remoteAttribute = typeof(T).GetCustomAttribute<AddToRemoteAttribute>();
            // ConfigValue config = remoteConfig.GetValue(remoteAttribute.searchName);
            // value = JsonConvert.DeserializeObject<T>(config.StringValue);
            value = jsonLibrary.Defaults.FirstOrDefault(json => json.GetType() == type);
            return value != null;
        }
    }
}