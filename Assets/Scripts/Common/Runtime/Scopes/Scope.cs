using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Modules.Scopes
{
    [UsedImplicitly]
    public class Scope<T>
    {
        private readonly Dictionary<Type, T> elements;

        public Scope(IEnumerable<T> elements)
        {
            this.elements = elements.ToDictionary(
                element => element.GetType().GetCustomAttribute<ScopedByAttribute>().type,
                element => element);
        }

        public void Invoke<TT>(TT value, Action<TT, T> callback)
        {
            if (elements.TryGetValue(value.GetType(), out T element))
            {
                callback(value, element);
            }
        }

        public TResult Require<TValue, TResult>(TValue value, Func<TValue, T, TResult> predicate)
        {
            if (elements.TryGetValue(value.GetType(), out T element))
            {
                return predicate(value, element);
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}