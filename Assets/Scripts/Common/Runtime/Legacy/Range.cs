using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.Common.Structures
{
    [Serializable]
    public struct Range
    {
        public float min;
        public float max;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public float GetRandom() => Random.Range(min, max);
        public bool Contains(float value) => min <= value && value <= max;

        public float Lerp(float t) => Mathf.Lerp(min, max, t);
    }
}
