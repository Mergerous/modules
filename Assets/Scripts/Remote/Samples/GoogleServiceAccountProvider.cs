using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using UnityEngine;

namespace Modules.Remote
{
    [Serializable]
    public sealed class ServiceAccountProvider : ICredentialsProvider
    {
        [SerializeField]
        private string _applicationName;
    
        [TextArea]
        [SerializeField]
        private string _credentialsJson;

        public SheetsService GetSheetsService()
        {
            var httpClientInitializer = GoogleCredential
                .FromJson(_credentialsJson)
                .CreateScoped(SheetsService.Scope.Spreadsheets);
        
            var initializer = new BaseClientService.Initializer
            {
                ApplicationName = _applicationName,
                HttpClientInitializer = httpClientInitializer
            };
            var sheetsService = new SheetsService(initializer);

            return sheetsService;
        }
    }
}
