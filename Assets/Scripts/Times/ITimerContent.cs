namespace Times
{
    public interface ITimerContent
    {
        T AddTimer<T>() where T : ITimer, new();
        void RemoveTimer(ITimer timer);
    }
}