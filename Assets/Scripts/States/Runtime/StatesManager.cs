using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

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
            state.Close();
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
    }
    
    [UsedImplicitly]
    public sealed class StatesManager
    {
        private StateNode baseNode = new(default);
        private Dictionary<int, StateMachine> machines = new();
        private IEnumerable<IState> statesList;
        
        public void Initialize(IEnumerable<IState> states)
        {
            statesList = states;
        }

        public StateMachine GetMachine(int index)
        {
            if (!machines.TryGetValue(index, out StateMachine machine))
            {
                machines.Add(index, machine = new StateMachine());
            }

            return machine;
        }

        public void Open<T>(StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
            where T : IState => Open(typeof(T), options, layer);

        public void Open(Type type, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
        {
            IState state = statesList.First(state => state.GetType() == type);
            Prepare(state, options, layer);
            state.Open();
        }
        
        public void Open<T, TPayload>(TPayload payload, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0) 
            where T : IState<TPayload>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Prepare(state, options, layer);
            state.Open();
        }

        public async Task OpenAsync<T>(CancellationToken cancellationToken, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
            where T : IState => await OpenAsync(typeof(T), cancellationToken, options, layer);

        public async Task OpenAsync(Type type, CancellationToken cancellationToken, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
        {
            IState state = statesList.First(state => state.GetType() == type);
            Prepare(state, options, layer);
            await state.OpenAsync(cancellationToken);
        }
        
        public async Task OpenAsync<T, TPayload>(TPayload payload, CancellationToken cancellationToken, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0) 
            where T : IState<TPayload>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Prepare(state, options, layer);
            await state.OpenAsync(cancellationToken);
        }
        
        public async Task<TResult> OpenAsync<T, TResult>(CancellationToken cancellationToken, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0) 
            where T : IResultState<TResult>
        {
            T state = statesList.OfType<T>().First();
            Prepare(state, options, layer);
            TResult result = await state.OpenAsync(cancellationToken);
            return result;
        }
        
        public async Task<TResult> OpenAsync<T, TPayload, TResult>(TPayload payload, CancellationToken cancellationToken, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0) 
            where T : IResultState<TPayload, TResult>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Prepare(state, options, layer);
            TResult result = await state.OpenAsync(cancellationToken);
            return result;
        }

        private void Prepare(IState item, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
        {
            StateNode node = baseNode;
            
            for (int i = 0; i < layer; i++)
            {
                StateNode childNode;
                if (node.nodes.Count > 0)
                {
                    childNode = node.nodes.Last();
                    childNode.state.Close();
                }
                
                childNode = new StateNode(item);
                node.nodes.AddLast(childNode);
                node = childNode;
            }
        }

        public void OpenLast(int layer = 0)
        {
            StateNode node = baseNode;

            for (int i = 0; i < layer; i++)
            {
                if (node.nodes.Count > 0)
                {
                    node = node.nodes.Last();
                }
            }
            
            node.Open();
        }

        public void CloseLast(int layer = 0)
        {
            StateNode node = baseNode;
            
            for (int i = 0; i < layer; i++)
            {
                if (node.nodes.Count > 0)
                {
                    node = node.nodes.Last();
                }
            }
            
            node.Close();
            baseNode.Remove(node);
        }

        public void OpenPrevious(int layer = 0)
        {
            CloseLast(layer);
            OpenLast(layer);
        }
    }
}