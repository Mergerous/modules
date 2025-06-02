using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using UnityEngine;

namespace Modules.Remote
{
    [Serializable]
    public sealed class GoogleServiceAccountProvider : ICredentialsProvider
    {
        [SerializeField]
        private string applicationName;
        
        [SerializeField]
        private TextAsset credentialsJson;
        
        public BaseClientService.Initializer GetClientService()
        {
            var httpClientInitializer = GoogleCredential
                .FromJson(credentialsJson.text)
                .CreateScoped(SheetsService.Scope.Drive, SheetsService.Scope.Spreadsheets);
        
            var initializer = new BaseClientService.Initializer
            {
                ApplicationName = applicationName,
                HttpClientInitializer = httpClientInitializer
            };
            
            return initializer;
        }
    }
}
