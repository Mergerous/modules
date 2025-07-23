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

        private void Prepare(IState item, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
        {
            if (options.HasFlag(StateOptions.ClosePrevious))
            {
                Close(layer);
            }
            if (options.HasFlag(StateOptions.RemovePreviousFromStack))
            {
                Clear(layer);
            }
            if (options.HasFlag(StateOptions.AddToStack))
            {
                AddToStack(layer, item);
            }
            if (options.HasFlag(StateOptions.LinkWithLastOnStack))
            {
                if (!machines.TryGetValue(layer, out StateMachine machine) || machine.stack.Count <= 0)
                {
                    return;
                }
                 
                if (!machine.linkedStates.TryGetValue(machine.stack.Peek(), out HashSet<IState> linked))
                {
                    linked = new HashSet<IState>();
                    machine.linkedStates.Add(machine.stack.Peek(), linked);
                }

                linked.Add(item);
            }
        }
        

        private void AddToStack(int layer, IState item)
        {
            StateMachine machine = GetMachine(layer);
            
            machine.stack.Push(item);
        }
        
        public void Close<T>(StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
            where T : IState
        {
            StateMachine machine = GetMachine(layer);
            if (machine.stack.Count > 0)
            {
                if (options.HasFlag(StateOptions.LinkWithLastOnStack))
                {
                    IState state = machine.stack.Peek();
                    if (machine.linkedStates.TryGetValue(state, out HashSet<IState> linked))
                    {
                        foreach (IState linkedState in linked)
                        {
                            if (linkedState.GetType() == typeof(T))
                            {
                                linkedState.Close();
                                linked.Remove(linkedState);
                                state.OnLinkedStateClosed();
                                break;
                            }
                        }
                    }
                }

                if (options.HasFlag(StateOptions.AddToStack))
                {
                    IState state = machine.stack.Pop();
                    state.Close();
                }
            }

        }

        public void ClearStack()
        {
            foreach ((int layer, var machine) in machines)
            {
                foreach (var state in machine.stack)
                {
                    if (machine.linkedStates.TryGetValue(state, out var linked))
                    {
                        foreach (var linkedState in linked)
                        {
                            linkedState.Close();
                        }
                    }
                    state.Close();
                }
                
                machine.stack.Clear();
            }
        }

        private void Clear(int sourceLayer)
        {
            foreach ((int layer, var machine) in machines)
            {
                if (layer >= sourceLayer)
                {
                    foreach (var state in machine.stack)
                    {
                        if (machine.linkedStates.TryGetValue(state, out var linked))
                        {
                            foreach (var linkedState in linked)
                            {
                                linkedState.Close();
                            }
                        }
                        state.Close();
                    }
                    foreach (var nonOrderedState in machine.nonOrderedStates)
                    {
                        nonOrderedState.Close();
                    }
                    
                    machine.nonOrderedStates.Clear();
                    machine.linkedStates.Clear();
                    machine.stack.Clear();
                }
            }
        }


        private void Close(int sourceLayer)
        {
            foreach ((int layer, var machine) in machines)
            {
                if (layer >= sourceLayer)
                {
                    if(machine.stack.TryPeek(out var state))
                    {
                        if (machine.linkedStates.TryGetValue(state, out var linked))
                        {
                            foreach (var linkedState in linked)
                            {
                                linkedState.Close();
                            }
                        }
                        state.Close();
                    }
                    
                    foreach (var nonOrderedState in machine.nonOrderedStates)
                    {
                        nonOrderedState.Close();
                    }
                }
            }
        }

        public void OpenLast(int layer = 0)
        {
            StateMachine machine = GetMachine(layer);
            
            if (machine.stack.Count > 0)
            {
                IState state = machine.stack.Peek();
                state.Open();
                if (machine.linkedStates.TryGetValue(state, out HashSet<IState> set))
                {
                    foreach (IState linked in set)
                    {
                        linked.Open();
                    }
                }
            }
        }

        public bool CloseLast(int layer = 0)
        {
            StateMachine machine = GetMachine(layer);
            
            if (machine.stack.Count > 0)
            {
                IState state = machine.stack.Pop();
                state.OnReturn();
                state.Close();

                if (machine.linkedStates.TryGetValue(state, out HashSet<IState> set))
                {
                    foreach (IState linked in set)
                    {
                        linked.OnReturn();
                        linked.Close();
                    }
                    set.Clear();
                }

                return true;
            }

            return false;
        }

        public void OpenPrevious(int layer = 0)
        {
            if (CloseLast(layer))
            {
                OpenLast(layer);
            }
        }
    }
}