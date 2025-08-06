using System.Collections.Generic;
using System.Linq;

namespace Modules.States
{
    public sealed class StateNode
    {
        public readonly List<IState> states;
        public readonly LinkedList<StateNode> nodes = new();

        public StateNode(params IState[] states)
        {
            this.states = new List<IState>(states);
        }

        public void Open()
        {
            foreach (var state in states)
            {
                state.Open();
            }
            
            foreach (StateNode childNode in nodes)
            {
                childNode.Open();
            }
        }

        public void Close()
        {
            foreach (var state in states)
            {
                state.Close();
            }
            
            foreach (StateNode childNode in nodes)
            {
                childNode.Close();
            }
        }

        public void Remove(StateNode node)
        {
            if (!nodes.Remove(node))
            {
                foreach (StateNode childNode in nodes)
                {
                    childNode.Remove(node);
                }
            }
        }

        public bool TryGetLayer<T>(int startLayer, out int layer)
            where T : IState
        {
            if (states.OfType<T>().Any())
            {
                layer = startLayer;
                return true;
            }

            foreach (StateNode node in nodes)
            {
                if (node.TryGetLayer<T>(startLayer + 1, out layer))
                {
                    return true;
                }
            }
            
            layer = startLayer;
            return false;
        }
    }
}