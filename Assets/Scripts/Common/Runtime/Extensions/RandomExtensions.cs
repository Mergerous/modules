using UnityEngine;

namespace Modules.Common.Extensions
{
    public static class RandomExtensions
    {
        private const int RANDOM_FLOOR = 0;
        private const int RANDOM_CEIL = 1;
        
        public static bool Bool() => Random.Range(0, 2) == 1;

        public static Vector2 Vector2(float min, float max) =>
            new Vector2(Random.Range(min, max), Random.Range(min, max));
        
        public static Vector3 Vector3(float min, float max) =>
            new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        
        public static Vector4 Vector4(float min, float max) =>
            new Vector4(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }
}
