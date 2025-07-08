using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Loading
{
    [UsedImplicitly]
    public sealed class LoadingManager
    {
        private readonly ILoadingProgress loadingProgress;
        private readonly IEnumerable<ILoadable> loadables;

        public LoadingManager(ILoadingProgress loadingProgress, IEnumerable<ILoadable> loadables)
        {
            this.loadingProgress = loadingProgress;
            this.loadables = loadables;
        }

        public void Prepare()
        {
            foreach (ILoadable loadable in loadables)
            {
                loadable.Prepare(loadingProgress);
            }
        }

        public async Task LoadAsync(CancellationToken cancellationToken)
        {

            
            foreach (ILoadable loadable in loadables)
            {
                await loadable.LoadAsync(loadingProgress, cancellationToken);
            }
        }
    }
}