using UnityEngine;

namespace Modules.Pool
{
    public static class PoolExtensions
    {
        private static Pool _pool;
        
        public static Pool Pool
        {
            set => _pool = value;
        }

        public static void Despawn(this IPoolable self)
        {
            if (self is MonoBehaviour mono)
            {
                _pool.Despawn(mono.gameObject);
            }
        }
    }
}