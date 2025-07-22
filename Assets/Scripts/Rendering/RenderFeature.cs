using System;
using Modules.Loop;
using UnityEngine;

namespace Modules.Rendering
{
    [Serializable]
    public abstract class RenderFeature
    {
        [SerializeField] protected string name;
        public string Name => name;

        public abstract void Initialize();
    }
}