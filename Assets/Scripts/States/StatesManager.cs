using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Modules.States
{
    [UsedImplicitly]
    public sealed class StatesManager
    {
        private Dictionary<int, StateMachine> machines = new();
        private Dictionary<string, IState> states = new();
        private IEnumerable<IState> statesList;

        public void Initialize(IEnumerable<IState> states)
        {
            statesList = states;
            foreach (IState state in states)
            {
                if (state.TryGetKey(out string key))
                {
                    this.states.Add(key, state);
                }
            }
        }

        public StateMachine GetMachine(int index)
        {
            if (!machines.TryGetValue(index, out StateMachine machine))
            {
                machines.Add(index, machine = new StateMachine());
            }

            return machine;
        }
        
        public StatesManager Open(string key, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
        {
            if (states.TryGetValue(key, out IState item))
            {
                Open(item, options, layer);
            }

            return this;
        }

        public StatesManager Open<T>(StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0) 
            where T : IState
        {
            T state = statesList.OfType<T>().First();
            Open(state, options, layer);
            return this;
        }
        
        public StatesManager Open<T, TPayload>(TPayload payload, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0) 
            where T : IState<TPayload>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Open(state, options, layer);
            return this;
        }

        public StatesManager Open<T>(string key, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0, T arguments = default)
        {
            if (states.TryGetValue(key, out IState item) && item is IState<T> argumentItem)
            {
                argumentItem.Payload = arguments;
                Open(argumentItem, options, layer);
            }
            
            return this;
        }

        public void Reopen(string key)
        {
            if (states.TryGetValue(key, out IState item))
            {
                item.OnReopen();
            }
        }

        private void Open(IState item, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
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

            item.Open();
        }
        

        private void AddToStack(int layer, IState item)
        {
            StateMachine machine = GetMachine(layer);
            
            machine.stack.Push(item);
        }
        
        public void Close<T>(StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
            where T : IState
        {
            if (options.HasFlag(StateOptions.LinkWithLastOnStack))
            {
                StateMachine machine = GetMachine(layer);
                if (machine.stack.Count > 0)
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
            }
        }

        public void Close(string key, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
        {
            if (options.HasFlag(StateOptions.LinkWithLastOnStack))
            {
                StateMachine machine = GetMachine(layer);
                if (machine.stack.Count > 0)
                {
                    IState state = machine.stack.Peek();
                    if (machine.linkedStates.TryGetValue(state, out HashSet<IState> linked))
                    {
                        foreach (IState linkedState in linked)
                        {
                            StateAttribute attribute = linkedState.GetType().GetCustomAttribute<StateAttribute>();
                            if (attribute != null && attribute.Key == key)
                            {
                                linkedState.Close();
                                linked.Remove(linkedState);
                                state.OnLinkedStateClosed();
                                break;
                            }
                        }
                    }
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