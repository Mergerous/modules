using System.Collections.Generic;

namespace Modules.States
{
    public sealed class StateMachine
    {
        public Stack<HashSet<IState>> stack;
        public List<StateMachine> child;

        public StateMachine()
        {
            stack = new Stack<HashSet<IState>>();
            child = new();
        }
    }
}