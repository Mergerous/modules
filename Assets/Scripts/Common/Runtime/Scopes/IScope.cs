using System;

namespace Modules.Scopes
{
    public interface IScope<out T>
    {
        public T Get(Type value);
        public T Get<TT>();
    }
}