using System;
using UnityEngine;

namespace Modules.Common.Structures
{
    [Serializable]
    public struct Resource<TValue>
        where TValue : struct
    {
        public TValue value;
        public TValue max;

        public Resource(TValue value, TValue max)
        {
            this.value = value;
            this.max = max;
        }
        
        public static implicit operator Resource<TValue>(TValue value) => new(value, value);

        public static implicit operator TValue(Resource<TValue> value) => value.value;
    }

    public static class ResourceExtensions
    {
        public static Resource<TValue> Ceil<TValue>(this Resource<TValue> source) where TValue : struct => new (source.max, source.max);
        
        public static Resource<TValue> Floor<TValue>(this Resource<TValue> source) where TValue : struct => new (default, source.max);

        public static float Ratio(this Resource<float> source) => source.value / source.max;

        public static float Ratio(this Resource<int> source, bool clamp = true)
        {
            if (clamp)
            {
                return Mathf.Clamp01((float)source.value / source.max);
            }
            
            return (float)source.value / source.max;
        }
        
        public static bool IsFull(this Resource<int> source) => source.value >= source.max;
    }
}