using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Modules.States
{
    [UsedImplicitly]
    public sealed class StatesManager
    {
        private StateNode baseNode = new(default);
        private IEnumerable<IState> statesList;
        
        public void Initialize(IEnumerable<IState> states)
        {
            statesList = states;
        }

        public void Open<T>(int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
            where T : IState => Open(typeof(T), layer, options);

        public void Open(Type type, int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
        {
            IState state = statesList.First(state => state.GetType() == type);
            Prepare(state, false, layer, options);
            state.Open();
        }
        
        public void Open<T, TPayload>(TPayload payload, int layer = 0, StateOptions options = StateOptions.CloseAndRemove) 
            where T : IState<TPayload>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Prepare(state, false, layer, options);
            state.Open();
        }

        public async Task OpenAsync<T>(CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
            where T : IState => await OpenAsync(typeof(T), cancellationToken, layer, options);

        public async Task OpenAsync(Type type, CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
        {
            IState state = statesList.First(state => state.GetType() == type);
            Prepare(state, true, layer, options);
            await state.OpenAsync(cancellationToken);
        }
        
        public async Task OpenAsync<T, TPayload>(TPayload payload, CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove) 
            where T : IState<TPayload>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Prepare(state, true, layer, options);
            await state.OpenAsync(cancellationToken);
        }
        
        public async Task<TResult> OpenAsync<T, TResult>(CancellationToken cancellationToken, int layer = 0, StateOptions options = StateOptions.CloseAndRemove) 
            where T : IResultState<TResult>
        {
            T state = statesList.OfType<T>().First();
            Prepare(state, true, layer, options);
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

        private void Prepare(IState item, bool isAsync, int layer = 0, StateOptions options = StateOptions.CloseAndRemove)
        {
            if (baseNode.TryGetLast(layer, out StateNode node))
            {
                if (options.HasFlag(StateOptions.Close) && node.nodes.Count > 0)
                {
                    node.nodes.Last().Close();
                }
                
                if (options.HasFlag(StateOptions.Remove) && node.nodes.Count > 0)
                {
                    node.nodes.RemoveLast();
                }
                
                if (options.HasFlag(StateOptions.Join))
                {
                    if (node.nodes.Count > 0)
                    {
                        node.nodes.Last().stateInfos.Add(new StateNode.StateInfo());
                    }
                }
                else
                {
                    node.nodes.AddLast(new StateNode(new StateNode.StateInfo(isAsync, item)));  
                }
            }
        }
        
        public void OpenPrevious<T>() where T : IState
        {
            if (baseNode.TryFindNode<T>(out LinkedListNode<StateNode> node))
            {
                node.Value.Close();
                node.Previous?.Value.Open();
                node.List.Remove(node);
            }
        }
        
        public IE

        [Obsolete]
        public void OpenPrevious(int layer = 0)
        {
            CloseLast(layer);
            OpenLast(layer);
        }
        
        [Obsolete]
        public void OpenLast(int layer = 0)
        {
            if (baseNode.TryGetLast(layer, out StateNode node))
            {
                node.Open();
            }
        }
        
        [Obsolete]
        public void CloseLast(int layer = 0)
        {
            if (baseNode.TryGetLast(layer, out StateNode node))
            {
                node.Close();
                baseNode.Remove(node);
            }
        }
    }
}