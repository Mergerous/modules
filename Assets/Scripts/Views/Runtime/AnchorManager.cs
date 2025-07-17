using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class AnchorManager
    {
        private readonly Dictionary<string, Transform> anchors;
        private readonly Dictionary<string, Action<Transform>> callbacks;

        public AnchorManager()
        {
            anchors = new Dictionary<string, Transform>();
            callbacks = new Dictionary<string, Action<Transform>>();
        }
        
        public void AddAnchor(string anchorKey, Transform anchor)
        {
            if (!anchors.TryAdd(anchorKey, anchor))
            {
                anchors[anchorKey] = anchor;
            }

            if (callbacks.Remove(anchorKey, out Action<Transform> callback))
            {
                callback?.Invoke(anchor);
            }
        }

        public void RemoveAnchor(string anchorKey)
        {
            anchors.Remove(anchorKey);
        }

        public async Task<Transform> GetAnchorAsync(string key, CancellationToken cancellationToken)
        {
            Transform value;
            
            while (!anchors.TryGetValue(key, out value))
            {
                await Task.Yield();
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new Exception();
                }
            }

            return value;
        }

        public void GetAnchor(string key, Action<Transform> callback)
        {
            if (anchors.TryGetValue(key, out Transform value))
            {
                callback?.Invoke(value);
            }
            else
            {
                callbacks.TryAdd(key, callback);
            }
        }
    }
}