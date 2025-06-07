using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Pool
{
    public abstract class PoolSettings<TO> : ScriptableObject
    {
        public List<ContainerTuple> Tuples;

        [Serializable]
        public class ContainerTuple
        {
            public TO Prefab;
            public int Count;
        }
    }
}