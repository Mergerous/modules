namespace Modules.Remote
{
    public sealed class RemoteInfoHandle
    {
        private readonly object remoteInfo;

        public RemoteInfoHandle(object remoteInfo)
        {
            this.remoteInfo = remoteInfo;
        }

        public T GetRemoteInfo<T>() where T : class
            => remoteInfo as T;
    }
}