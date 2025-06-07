using System;
using System.Collections.Generic;

namespace Modules.Architecture.Systems
{
    public static class DisposableExtensions
    {
        public static void AddTo<T>(this IDisposable disposable, T key, IDictionary<T, IDisposable> disposables)
        {
            disposables.Add(key, disposable);
        }
    }
}