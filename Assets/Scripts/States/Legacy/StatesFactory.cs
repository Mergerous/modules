using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Modules.States
{
    [UsedImplicitly]
    public sealed class StatesFactory
    {
        public StatesFactory(IEnumerable<IState> stateHandlers, StatesManager statesManager)
        {
            // statesManager.Initialize(stateHandlers.ToDictionary(handler =>
            // {
            //     handler.TryGetKey(out var key);
            //     return key;
            // }, handler => handler));
        }
    }
}