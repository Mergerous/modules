using System.Collections.Generic;
using System.Linq;

namespace Modules.States
{
    public sealed class StateNode
    {
        public struct StateInfo
        {
            public bool isAsync;
            public IState state;

            public StateInfo(bool isAsync, IState state)
            {
                this.isAsync = isAsync;
                this.state = state;
            }
        }
        
        public readonly List<StateInfo> stateInfos;
        public readonly LinkedList<StateNode> nodes = new();

        public StateNode(params StateInfo[] stateInfos)
        {
            if (stateInfos != null)
            {
                this.stateInfos = new List<StateInfo>(stateInfos);
            }
            else
            {
                this.stateInfos = new();
            }
        }

        public void Open()
        {
            foreach (var stateInfo in stateInfos)
            {
                if (stateInfo.isAsync)
                {
                    state.OpenAsync();
                }
                else
                {
                    state.Open();
                }
            }
            
            foreach (StateNode childNode in nodes)
            {
                childNode.Open();
            }
        }

        public void Close()
        {
            foreach (var stateInfo in stateInfos)
            {
                stateInfo.state.Close();
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
                T result = child.stateInfos.FirstOrDefault(info => info.state.GetType() == typeof(T));
                
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