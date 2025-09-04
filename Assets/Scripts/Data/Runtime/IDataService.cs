using System;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Runtime
{
    public interface IDataService
    {
        void Save(string key, object data)
        {
            throw new NotImplementedException();
        }

        void SaveAsync(string key, object data, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        T LoadOrFallback<T>(string key, T fallback)
        {
            throw new NotImplementedException();
        }

        Task<T> LoadOrFallbackAsync<T>(string key, T fallback, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}