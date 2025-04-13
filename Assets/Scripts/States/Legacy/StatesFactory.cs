using System.Collections.Generic;
using JetBrains.Annotations;

namespace Modules.States
{
    [UsedImplicitly]
    public sealed class StatesFactory
    {
        public StatesFactory(IEnumerable<IState> stateHandlers, StatesManager statesManager)
        {
            statesManager.Initialize(stateHandlers);
        }
    }
}