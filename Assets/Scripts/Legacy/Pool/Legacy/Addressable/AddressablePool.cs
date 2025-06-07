#if ADDRESSABLES
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pool;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.Pool {
public class AddressablePool : Pool<AssetReference>, IDisposable
{
    public AddressablePool(AddressablePoolContainer poolContainer, Transform instanceContainer) : base(poolContainer, instanceContainer)
    {
    }
    public async Task<T> SpawnAsync<T>(AssetReference key)
    {
        if (!_pooledObjects.ContainsKey(key.AssetGUID))
        {
            Prepare(key, 1);
        }
        var pooledObject = _pooledObjects[key.AssetGUID].FirstOrDefault(x => x.isFree);

        if (pooledObject == null)
        {
            pooledObject = new PoolTuple(false, default);
            _pooledObjects[key.AssetGUID].Enqueue(pooledObject);
            var obj = await CreateObjectAsync(key);
            pooledObject.obj = obj;
        }
        else
        {
            pooledObject.isFree = false;
        }
     
        OnSpawned(pooledObject.obj);
            
        return pooledObject.obj.GetComponent<T>();
    }

    protected override async void Prepare(AssetReference pooledObject, int count)
    {
        if (_pooledObjects.TryGetValue(pooledObject, out Queue<PoolTuple> objectPool))
        {
            for (int i = 0; i < count - objectPool.Count; i++)
            {
                var tuple = new PoolTuple(true, default);
                objectPool.Enqueue(tuple);
                var obj = await CreateObjectAsync(pooledObject);
                tuple.obj = obj;
            }
        }
        else
        {
            objectPool = new Queue<PoolTuple>();
            _pooledObjects.Add(pooledObject.AssetGUID, objectPool);
            for (int i = 0; i < count; i++)
            {
                var obj = await CreateObjectAsync(pooledObject);
                objectPool.Enqueue(new PoolTuple(true, obj));
            }
        }
    }

    private async Task<GameObject> CreateObjectAsync(AssetReference reference)
    {
        var gameObject = await Addressables.InstantiateAsync(reference).Task;
        OnCreated(gameObject);
        return gameObject;
    }

    protected override GameObject CreateObject(AssetReference reference)
    {
        var gameObject = Addressables.Instantiate(reference).Result;
        OnCreated(gameObject);
        return gameObject;
    }

    public void Dispose()
    {
        foreach (var pair in _pooledObjects)
        {
            foreach (var tuple in pair.Value)
            {
                Addressables.ReleaseInstance(tuple.obj);
            }
        }
    }
}
}
#endif
