using R3;

namespace Loading
{
    public interface ILoadingProgress
    {
        Observable<float> ProgressObservable { get; }
        float TotalProgress { get; }
        void AddProgress(float progress);
        void AddTotalProgress(float totalProgress);
    }
}