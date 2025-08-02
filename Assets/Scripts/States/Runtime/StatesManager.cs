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

        public void Open<T>(int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
            where T : IState => Open(typeof(T), layer, options);

        public void Open(Type type, int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
        {
            IState state = statesList.First(state => state.GetType() == type);
            Prepare(state, layer, options);
            state.Open();
        }
        
        public void Open<T, TPayload>(TPayload payload, int layer = 0, StateOptions options = StateOptions.CloseAndRemove) 
            where T : IState<TPayload>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Prepare(state, layer, options);
            state.Open();
        }

        public async Task OpenAsync<T>(CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
            where T : IState => await OpenAsync(typeof(T), cancellationToken, layer, options);

        public async Task OpenAsync(Type type, CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
        {
            IState state = statesList.First(state => state.GetType() == type);
            Prepare(state, layer, options);
            await state.OpenAsync(cancellationToken);
        }
        
        public async Task OpenAsync<T, TPayload>(TPayload payload, CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove) 
            where T : IState<TPayload>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Prepare(state, layer, options);
            await state.OpenAsync(cancellationToken);
        }
        
        public async Task<TResult> OpenAsync<T, TResult>(CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove) 
            where T : IResultState<TResult>
        {
            T state = statesList.OfType<T>().First();
            Prepare(state, layer, options);
            TResult result = await state.OpenAsync(cancellationToken);
            return result;
        }
        
        public async Task<TResult> OpenAsync<T, TPayload, TResult>(TPayload payload, CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove) 
            where T : IResultState<TPayload, TResult>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Prepare(state, layer, options);
            TResult result = await state.OpenAsync(cancellationToken);
            return result;
        }

        private void Prepare(IState item, int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
        {
            StateNode node = baseNode;

            for (int i = 0; i < layer - 1; i++)
            {
                if (node.nodes.Count > 0)
                {
                    node = node.nodes.Last();
                }
            }

            if (node.nodes.Count > 0)
            {
                if (options.HasFlag(StateOptions.Close))
                {
                    node.nodes.Last().Close();
                }
                
                if (options.HasFlag(StateOptions.Remove))
                {
                    node.nodes.RemoveLast();
                }
            }
         
            node.nodes.AddLast(new StateNode(item));
        }

        public void OpenLast(int layer = 0)
        {
            StateNode node = baseNode;

            for (int i = 0; i <= layer; i++)
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
            
            for (int i = 0; i <= layer; i++)
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