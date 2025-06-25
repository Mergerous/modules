using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Modules.Views
{
    [UsedImplicitly]
    public interface IPresenterFactory : IDisposable
    {
        public T Create<T>() where T : Presenter;
        public IEnumerable<T> GetInstances<T>() where T : Presenter;
        public bool Remove(Presenter instance);
    }
}