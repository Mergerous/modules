namespace Modules.Loop.Interfaces
{
    public interface IUpdatable
    {
        UpdateType UpdateType => UpdateType.Update;
        void Update(float deltaTime);
    }
}
