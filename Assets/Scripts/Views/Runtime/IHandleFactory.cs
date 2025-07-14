namespace Modules.Views
{
    public interface IHandleFactory
    {
        IViewHandle CreateHandle(string key, bool shouldCache = false);
    }
}