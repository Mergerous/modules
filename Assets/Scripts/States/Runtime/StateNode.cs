using System.Collections.Generic;
using System.Linq;

namespace Modules.States
{
    public sealed class StateNode
    {
        public IState state;
        public LinkedList<StateNode> nodes = new();

        public StateNode(IState state)
        {
            this.state = state;
        }

        public void Open()
        {
            state.Open();
            foreach (StateNode childNode in nodes)
            {
                childNode.Open();
            }
        }

        public void Close()
        {
            state?.Close();
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
            if (state?.GetType() == typeof(T))
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