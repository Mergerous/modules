using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;

namespace Modules.Remote
{
    [UsedImplicitly]
    public sealed class AssetManager : IDisposable
    {
        private readonly HashSet<AssetReference> references = new();
        
        public async Task<T> LoadAssetAsync<T>(AssetReference reference, CancellationToken cancellationToken)
        {
            if (!references.Contains(reference))
            {
                reference.LoadAssetAsync<T>();
                references.Add(reference);
            }

            while (!reference.OperationHandle.IsDone)
            {
                await Task.Yield();
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new Exception();
                }
            }

            return reference.OperationHandle.Convert<T>().Result;
        }

        public void Dispose()
        {
            foreach (AssetReference reference in references)
            {
                reference.ReleaseAsset();
            }
            
            references.Clear();
        }
    }
}