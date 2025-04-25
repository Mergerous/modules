using System.Collections.Generic;

namespace Modules.States
{
    public sealed class StateMachine
    {
        public HashSet<IState> nonOrderedStates;
        public Stack<IState> stack;
        public Dictionary<IState, HashSet<IState>> linkedStates;

        public StateMachine()
        {
            nonOrderedStates = new HashSet<IState>();
            stack = new Stack<IState>();
            linkedStates = new Dictionary<IState, HashSet<IState>>();
        }
    }
}