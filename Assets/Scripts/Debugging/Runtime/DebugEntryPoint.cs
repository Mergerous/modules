using JetBrains.Annotations;
using Modules.Debugging;
using VContainer.Unity;

namespace Modules.Debugging
{
    [UsedImplicitly]
    public sealed class DebugEntryPoint : IStartable, ITickable
    {
        private readonly DebugManager debugManager;

        public DebugEntryPoint(DebugManager debugManager)
        {
            this.debugManager = debugManager;
        }

        public void Start()
        {
            debugManager.Execute();
        }
        
        public void Tick()
        {
            debugManager.Update();
        }
    }
}
