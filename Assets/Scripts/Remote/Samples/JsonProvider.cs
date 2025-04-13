using System;
using System.IO;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Remote
{
    [Serializable]
    public sealed class JsonProvider : IRequestProvider
    {
        [SerializeField]
        private string _spreadsheetId;
    
        [SerializeField]
        private string _range;

        [SerializeField] 
        private Object folder;

        public async void Request(SheetsService service)
        {
#if UNITY_EDITOR
            SpreadsheetsResource.ValuesResource.GetRequest request = new SpreadsheetsResource.ValuesResource.GetRequest(service, _spreadsheetId, _range);
            ValueRange response = await request.ExecuteAsync();
            await File.WriteAllTextAsync($"{AssetDatabase.GetAssetPath(folder)}/{_range}.json", JsonConvert.SerializeObject(response));
            
            Debug.Log("Export succeed");
#endif
        }
    }
}