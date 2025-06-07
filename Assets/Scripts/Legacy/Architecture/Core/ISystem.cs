namespace Modules.Architecture.Interfaces
{
    public interface ISystem
    {
        void Register(IEntity entity);
        void Unregister(IEntity entity);
    }
}