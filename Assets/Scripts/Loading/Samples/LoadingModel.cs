using JetBrains.Annotations;
using R3;

namespace Loading
{
    [UsedImplicitly]
    internal sealed class LoadingModel : ILoadingProgress
    {
        private ReactiveProperty<float> progress = new();
        public Observable<float> ProgressObservable => progress;
        public float TotalProgress { get; private set; }
        
        public void AddProgress(float progress)
        {
            this.progress.Value += progress;
        }

        public void AddTotalProgress(float totalProgress)
        {
            TotalProgress += totalProgress;
        }
    }
}