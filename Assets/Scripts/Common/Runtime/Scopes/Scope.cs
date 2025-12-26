using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Modules.Scopes
{
    [UsedImplicitly]
    public class Scope<T> : IScope<T>
    {
        private readonly Dictionary<Type, T> elements;

        public Scope(IEnumerable<T> elements)
        {
            this.elements = elements.ToDictionary(
                element => element.GetType().GetCustomAttribute<ScopedByAttribute>().type,
                element => element);
        }

        public T Get(Type value)
        {
            if (elements.TryGetValue(value, out T element))
            {
                return element;
            }

            return default;
        }
        
        public T Get<TT>()
        {
            if (elements.TryGetValue(typeof(TT), out T element))
            {
                return element;
            }

            return default;
        }

        public IEnumerable<T> Get(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (elements.TryGetValue(type, out var element))
                {
                    yield return element;
                }
            }
        }
    }
}