using System; 
using System.IO;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Remote
{
    [Serializable]
    public sealed class SheetsRequestProvider : IRequestProvider
    {
        [SerializeField] private string spreadsheetId;
        [SerializeField] private string gid;
        [SerializeField] private Object folder;
        [SerializeField] private string format = ".json";
        
        public async void Request(BaseClientService.Initializer initializer)
        {
#if UNITY_EDITOR
            SheetsService service = new SheetsService(initializer);
            SpreadsheetsResource.ValuesResource.GetRequest request = new SpreadsheetsResource.ValuesResource.GetRequest(service, spreadsheetId, gid);
            ValueRange response = await request.ExecuteAsync();
            await File.WriteAllTextAsync($"{AssetDatabase.GetAssetPath(folder)}/{gid}{format}", JsonConvert.SerializeObject(response));
            
            Debug.Log("Export succeed");
#endif
        }
    }
}