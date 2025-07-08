using System.Threading;
using System.Threading.Tasks;
using R3;

namespace Loading
{
    public sealed class LoadingPayload
    {
        private readonly Subject<Unit> onNext = new();
        private readonly CancellationToken cancellationToken;
        
        public Task OnNext => onNext .FirstAsync(cancellationToken);
        
        public void MoveNext() => onNext.OnNext(Unit.Default);

        public LoadingPayload(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
        }
    }
}