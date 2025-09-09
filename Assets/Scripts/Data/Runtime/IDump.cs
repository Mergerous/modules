namespace Modules.Data
{
    public interface IDump
    {
        public T GetDataOrDefault<T>();
    }
}