using Modules.Loop.Interfaces;
using Modules.Loop.Managers;

namespace Modules.Loop
{
    public static class LoopExtensions
    {
        private static LoopManager _loopManager;
        public static LoopManager LoopManager { set => _loopManager = value; }

        public static void Start(this IUpdatable updatable)
        {
            _loopManager.Add(updatable);
        }

        public static void Stop(this IUpdatable updatable)
        {
            _loopManager.Remove(updatable);
        }
    }
}
