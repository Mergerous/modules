using System;
using System.IO;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Remote
{
    [Serializable]
    public sealed class DriveRequestProvider : IRequestProvider
    {
        [SerializeField] private string fileId;
        [SerializeField] private Object folder;

        public async void Request(BaseClientService.Initializer initializer)
        {
#if UNITY_EDITOR
            DriveService service = new DriveService(initializer);
            MemoryStream stream = new MemoryStream();
            FilesResource.GetRequest request = service.Files.Get(fileId);
            var response = await request.ExecuteAsync();
            
            await request.DownloadAsync(stream);
            await File.WriteAllBytesAsync(AssetDatabase.GetAssetPath(folder) + "/" + response.Name, stream.ToArray());
            
            Debug.Log("Done"); 
#endif
        }
    }
}
