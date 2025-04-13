namespace Modules.Remote
{
    public interface IRemoteHandler
    {
        void OnFetched(RemoteInfoHandle handle);
    }
}
