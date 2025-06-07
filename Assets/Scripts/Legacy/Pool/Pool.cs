using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.Pool
{
    public abstract class Pool
    {
        public Pool()
        {
            PoolExtensions.Pool = this;
        }
        
        public abstract void Despawn(GameObject instance);
    }
    
    public abstract class Pool<O> : Pool
    {
        protected readonly Dictionary<object, Queue<PoolTuple>> _pooledObjects;
        
        protected readonly Transform _instanceContainer;
        protected readonly List<GameObject> _freezedObjects;

        public Pool(PoolSettings<O> poolSettings, Transform instanceContainer)
        {
            _instanceContainer = instanceContainer;
            _pooledObjects = new Dictionary<object, Queue<PoolTuple>>();
            _freezedObjects = new List<GameObject>();

            foreach (var tuple in poolSettings.Tuples)
            {
                Prepare(tuple.Prefab, tuple.Count);
            }
        }

        public virtual T Spawn<T>(O key)
        {
            if (!_pooledObjects.ContainsKey(key))
            {
                Prepare(key, 1);
            }
            var pooledObject = _pooledObjects[key].FirstOrDefault(x => x.isFree);

            if (pooledObject == null)
            {
                pooledObject = new PoolTuple(true, CreateObject(key));
                _pooledObjects[key].Enqueue(pooledObject);
            }
            pooledObject.isFree = false;
            OnSpawned(pooledObject.obj);
            
            return pooledObject.obj.GetComponent<T>();
        }


        public T Unfreeze<T>(GameObject instance)
        {
            if (_freezedObjects.Contains(instance))
            {
                _freezedObjects.Remove(instance);
                OnUnfreezed(instance);
                return instance.GetComponent<T>();
            }
            return default;
        }
        public void Freeze(GameObject instance)
        {
            if (!_freezedObjects.Contains(instance))
            {
                _freezedObjects.Add(instance);
                OnFreezed(instance);
            }
        }

        public void ResetAll(O prefab, bool destroy = false)
        {
            if (_pooledObjects.TryGetValue(prefab, out var queue))
            {
                foreach (var valueTuple in queue)
                {
                    if (destroy)
                    {
                        Object.Destroy(valueTuple.obj);
                        continue;
                    }
                    valueTuple.isFree = true;
                    OnReset(valueTuple.obj);
                }

                if (destroy)
                {
                    _pooledObjects.Remove(prefab);
                }
            }
        }

        public void DespawnAll(O pooledObject)
        {
            if (_pooledObjects.TryGetValue(pooledObject, out Queue<PoolTuple> objectPool))
            {
                foreach (var poolTuple in objectPool)
                {
                    Despawn(poolTuple.obj);
                }
            }
        }
        
        public override void Despawn(GameObject instance)
        {
            foreach (var values in _pooledObjects.Values)
            {
                foreach (var valueTuple in values)
                {
                    if (valueTuple.obj == instance)
                    {
                        valueTuple.isFree = true;
                        OnDespawned(valueTuple.obj);
                        return;
                    }
                }
            }
        }
        
        protected virtual void Prepare(O pooledObject, int count)
        {
            if (_pooledObjects.TryGetValue(pooledObject, out Queue<PoolTuple> objectPool))
            {
                for (int i = 0; i < count - objectPool.Count; i++)
                {
                    var obj = CreateObject(pooledObject);
                    objectPool.Enqueue(new PoolTuple(true, obj));
                }
            }
            else
            {
                objectPool = new Queue<PoolTuple>();

                for (int i = 0; i < count; i++)
                {
                    var obj = CreateObject(pooledObject);
                    objectPool.Enqueue(new PoolTuple(true, obj));
                }

                _pooledObjects.Add(pooledObject, objectPool);
            }
        }

        protected abstract GameObject CreateObject(O reference);
        protected virtual void OnReset(GameObject item)
        {
            item.SetActive(false);
        }

        protected virtual void OnSpawned(GameObject item)
        {
            item.SetActive(true);
        }        
        
        protected virtual void OnUnfreezed(GameObject item)
        {
            item.SetActive(true);
        }
        
        protected virtual void OnDespawned(GameObject item)
        {
            item.SetActive(false);
        }

        protected virtual void OnCreated(GameObject item)
        {
            item.SetActive(false);
            item.transform.SetParent(_instanceContainer);
        }

        protected virtual void OnFreezed(GameObject item)
        {
            item.SetActive(false);
        }
        protected class PoolTuple
        {
            public PoolTuple(bool isFree, GameObject obj)
            {
                this.isFree = isFree;
                this.obj = obj;
            }
            public bool isFree;
            public GameObject obj;
        }
    }
}