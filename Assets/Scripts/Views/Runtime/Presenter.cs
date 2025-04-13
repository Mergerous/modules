using System;
using System.Collections.Generic;

namespace Modules.Views
{
    public abstract class Presenter
    {
        protected readonly ICollection<IDisposable> disposables = new List<IDisposable>();

        public virtual void Subscribe()
        {
            
        }

        public virtual void Unsubscribe()
        {
            foreach (IDisposable disposable in disposables)
            {
                disposable.Dispose();
            }

            disposables.Clear();
        }
    }
}