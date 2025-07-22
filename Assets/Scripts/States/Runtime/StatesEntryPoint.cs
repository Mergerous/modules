using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.States;
using VContainer.Unity;

namespace States
{
    [UsedImplicitly]
    public sealed class StatesEntryPoint : IInitializable
    {
        private readonly StatesManager statesManager;
        private readonly IEnumerable<IState> states;
        
        public StatesEntryPoint(StatesManager statesManager, IEnumerable<IState> states)
        {
            this.statesManager = statesManager;
            this.states = states;
        }

        public void Initialize()
        {
            statesManager.Initialize(states);
        }
    }
}