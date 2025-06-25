using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using VContainer;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class PresenterFactory : IPresenterFactory
    {
        private readonly List<Presenter> instances = new();
        private readonly IObjectResolver resolver;
        
        public PresenterFactory(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }
        
        public T Create<T>() where T : Presenter
        {
            T instance = resolver.Resolve<T>();
            instances.Add(instance);
            return instance;
        }

        public IEnumerable<T> GetInstances<T>() where T : Presenter => instances.OfType<T>();

        public bool Remove(Presenter instance)
        {
            instance.Unsubscribe();
            return instances.Remove(instance);
        } 

        public void Dispose()
        {
            foreach (Presenter instance in instances)
            {
                instance.Unsubscribe();
            }
            
            instances.Clear();
        }
    }
}