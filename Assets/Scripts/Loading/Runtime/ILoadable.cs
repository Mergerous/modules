using System.Threading;
using System.Threading.Tasks;

namespace Loading
{
    public interface ILoadable
    {
        void Prepare(ILoadingProgress progress);
        Task LoadAsync(ILoadingProgress progress, CancellationToken cancellationToken);
    }
}