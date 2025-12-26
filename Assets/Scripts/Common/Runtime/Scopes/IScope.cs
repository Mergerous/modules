using System;
using System.Collections.Generic;

namespace Modules.Scopes
{
    public interface IScope<out T>
    {
        public T Get(Type value);
        public T Get<TT>();
        public IEnumerable<T> Get(IEnumerable<Type> types);
    }
}