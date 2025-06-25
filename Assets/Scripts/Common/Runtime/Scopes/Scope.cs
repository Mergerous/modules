using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Modules.Scopes
{
    [UsedImplicitly]
    public sealed class Scope<T>
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
            if (elements.TryGetValue(typeof(TT), out T element))
            {
                callback(value, element);
            }
        }
    }
}