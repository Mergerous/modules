using UnityEngine;

namespace Modules.Pool
{
    public class MonoPool : Pool<GameObject>
    {
        public MonoPool(MonoPoolSettings poolSettings, Transform instanceContainer) : base(poolSettings,
            instanceContainer)
        {
        }
        
        protected override GameObject CreateObject(GameObject reference)
        {
            GameObject obj = Object.Instantiate(reference, _instanceContainer);
            OnCreated(obj);
            return obj;
        }
        
        public T Spawn<T>(GameObject prefab, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            var instance = Spawn<T>(prefab);
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }
        public T Respawn<T>(GameObject instance, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            var i = Unfreeze<T>(instance);
            instance.transform.SetPositionAndRotation(position, rotation);
            return i;
        }
    }
}