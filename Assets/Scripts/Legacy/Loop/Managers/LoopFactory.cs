using Modules.Loop.Interfaces;

namespace Modules.Loop.Managers
{
    public sealed class LoopFactory
    {
        //TODO Safe add IExecutable
        private readonly IUpdatable[] updatables;
        private readonly IExecutable[] executables;
        
        public LoopFactory(LoopManager loopManager, IExecutable[] executables, IUpdatable[] updatables)
        {
            this.updatables = updatables;
            this.executables = executables;

            foreach (IUpdatable updatable in updatables)
            {
                loopManager.Add(updatable);
            }

            foreach (IExecutable executable in executables)
            {
                loopManager.Add(executable);
            }
        }
    }
}
