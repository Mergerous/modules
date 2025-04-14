using System;

namespace Views.Samples
{
    public sealed class Cache<T> : IDisposable
    {
        private T value;
        private event Action<T> destroyAction;
        private event Func<T> createFactory;

        public T Value => value ??= createFactory.Invoke();

        public bool IsValueCreated => value != null;
        
        public Cache(Func<T> createFactory, Action<T> destroyAction)
        {
            this.createFactory = createFactory;
            this.destroyAction = destroyAction;
        }

        public void CreateValue()
        {
            value ??= createFactory.Invoke();
        }
        
        public void Dispose()
        {
            if (value != null)
            {
                destroyAction?.Invoke(value);
                value = default;
            }
        }
    }
}