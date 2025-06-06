using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Common.Extensions
{
    public static class CommonExtensions
    {
        public static bool IncludesLayer(this LayerMask source, int layer) => (source.value & 1 << layer) > 0;


        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] source)
        {
            foreach (IEnumerable<T> enumerable in source)
            {
                foreach (T value in enumerable)
                {
                    yield return value;
                }
            }
        }

        public static T MaxBy<T>(this IEnumerable<T> source, Func<T, float> predicate)
        {
            float max = default;
            T maxModel = default;
            
            foreach (T model in source)
            {
                float current = predicate(model);
                if (current > max)
                {
                    max = current;
                    maxModel = model;
                }
            }

            return maxModel;
        }

        public static T Next<T>(this T source) where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            int index = Array.IndexOf(values, source);
            object destination = index < values.Length ? values.GetValue(values.Length - 1) : values.GetValue(index);
            return (T)destination;
        }

        public static bool IsNullOrEmpty(this ICollection source)
        {
            return source is not { Count: > 0 };
        }


        public static Vector3 GetWorldPosition(this RectTransform source)
        {
            Vector3[] corners = new Vector3[4];
            source.GetWorldCorners(corners);
            Vector3 center = (corners[0] + corners[2]) / 2f;
            return center;
        }
        
        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            // This returns the world space positions of the corners in the order
            // [0] bottom left,
            // [1] top left
            // [2] top right
            // [3] bottom right
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Vector2 min = corners[0];
            Vector2 max = corners[2];
            Vector2 size = max - min;
 
            return new Rect(min, size);
        }
 
        ///<summary>
        /// Checks if a <see cref="RectTransform"/> fully encloses another one
        ///</summary>
        public static bool FullyContains (this RectTransform rectTransform, RectTransform other)
        {       
            var rect = rectTransform.GetWorldRect();
            var otherRect = other.GetWorldRect();

            // Now that we have the world space rects simply check
            // if the other rect lies completely between min and max of this rect
            return rect.xMin <= otherRect.xMin 
                   && rect.yMin <= otherRect.yMin 
                   && rect.xMax >= otherRect.xMax 
                   && rect.yMax >= otherRect.yMax;
        }
    }
}
