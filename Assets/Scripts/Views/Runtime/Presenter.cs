using System;
using System.Collections.Generic;
using System.Threading;

namespace Modules.Views
{
    public abstract class Presenter
    {
        protected readonly ICollection<IDisposable> disposables = new List<IDisposable>();
        protected readonly CancellationTokenSource cancellationTokenSource = new();

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
            
            cancellationTokenSource.Dispose();
        }
    }
}