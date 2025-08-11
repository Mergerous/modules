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
            if (states != null)
            {
                this.states = new List<IState>(states);
            }
            else
            {
                this.states = new();
            }
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

        public bool TryFindNode<T>(out LinkedListNode<StateNode> node) where T : IState
        {
            foreach (StateNode child in nodes)
            {
                T result = child.states.OfType<T>().FirstOrDefault();
                
                if (result != null)
                {
                    node = nodes.Find(child);
                    return true;
                }
                
                if(child.TryFindNode<T>(out node))
                {
                    return true;
                }
            }

            node = default;
            return false;
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

        public bool TryGetLast(int layer, out StateNode node)
        {
            if (layer == 0)
            {
                node = this;
                return true;
            }
            if (nodes.Count > 0)
            {
                return nodes.Last.Value.TryGetLast(layer - 1, out node);
            }

            node = default;
            return false;
        }
    }
}