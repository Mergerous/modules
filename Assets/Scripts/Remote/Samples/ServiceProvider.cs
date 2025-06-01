using Google.Apis.Sheets.v4;
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
            SheetsService service = credentialsProvider.GetSheetsService();

            foreach (IRequestProvider requestProvider in requestProviders)
            {
                requestProvider.Request(service);
            }
        }
#endif
    }
}
