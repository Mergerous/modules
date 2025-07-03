using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace Modules.CommonModule.Extensions 
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            Random random = new Random();
            return source.OrderBy(_ => random.Next());
        }

        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            return source.Shuffle().First();
        }

        public static bool TryFindOfType<T>(this IEnumerable source, out T destination)
        {
            foreach (object element in source)
            {
                if (element is T result)
                {
                    destination = result;
                    return true;
                }
            }

            destination = default;
            return false;
        }

        public static bool TryFind<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T destination)
        {
            foreach (T element in source)
            {
                if (predicate.Invoke(element))
                {
                    destination = element;
                    return true;
                }
            }

            destination = default;
            return false;
        }
        
        public static T Find<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T element in source)
            {
                if (predicate.Invoke(element))
                {
                    return element;
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        public static bool Contains(this Array source, object element)
        {
            return Array.IndexOf(source, element) >= 0;
        }
    }
}