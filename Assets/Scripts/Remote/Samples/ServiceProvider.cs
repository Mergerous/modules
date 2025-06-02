using Google.Apis.Services;
using Modules.Remote;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remote
{
    [CreateAssetMenu]
    public sealed class ServiceProvider : SerializedScriptableObject
    {
        [SerializeReference] private ICredentialsProvider credentialsProvider;
        [SerializeReference] private IRequestProvider[] requestProviders;
        
#if UNITY_EDITOR
        [Button]
        public void Parse()
        {
            BaseClientService.Initializer service = credentialsProvider.GetClientService();

            foreach (IRequestProvider requestProvider in requestProviders)
            {
                requestProvider.Request(service);
            }
        }
#endif
    }
}
