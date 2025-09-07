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

        T LoadOrDefault<T>(string key)
        {
            throw new NotImplementedException();
        }

        Task<T> LoadOrDefaultAsync<T>(string key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}