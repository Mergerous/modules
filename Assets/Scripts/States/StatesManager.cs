using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Modules.States
{
    [UsedImplicitly]
    public sealed class StatesManager
    {
        private Dictionary<int, HashSet<IState>> nonOrderedStates;
        private Dictionary<int, Stack<IState>> orderedStates;
        private Dictionary<IState, HashSet<IState>> linkedStates;
        private Dictionary<string, IState> states;
        private IEnumerable<IState> statesList;

        public void Initialize(IEnumerable<IState> states)
        {
            nonOrderedStates = new Dictionary<int,HashSet<IState>>();
            orderedStates = new Dictionary<int, Stack<IState>>();
            linkedStates = new Dictionary<IState, HashSet<IState>>();
            statesList = states;
            foreach (IState state in states)
            {
                if (state.TryGetKey(out string key))
                {
                    this.states.Add(key, state);
                }
            }
        }
        
        public StatesManager Open(string key, StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
        {
            if (states.TryGetValue(key, out IState item))
            {
                Open(item, options, layer);
            }

            return this;
        }

        public StatesManager Open<T>(StateOptions options = StateOptions.ClosePreviousAndAddToStack) 
            where T : IState
        {
            T state = statesList.OfType<T>().First();
            Open(state, options);
            return this;
        }
        
        public StatesManager Open<T, TPayload>(TPayload payload, StateOptions options = StateOptions.ClosePreviousAndAddToStack) 
            where T : IState<TPayload>
        {
            T state = statesList.OfType<T>().First();
            state.Payload = payload;
            Open(state, options);
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
                if (!orderedStates.TryGetValue(layer, out Stack<IState> stack) || stack.Count <= 0)
                {
                    return;
                }
                 
                if (!linkedStates.TryGetValue(stack.Peek(), out HashSet<IState> linked))
                {
                    linked = new HashSet<IState>();
                    linkedStates.Add(stack.Peek(), linked);
                }

                linked.Add(item);
            }

            item.Open();
        }
        

        private void AddToStack(int layer, IState item)
        {
            if (!orderedStates.TryGetValue(layer, out Stack<IState> stack))
            {
                stack = new Stack<IState>();
                orderedStates.Add(layer, stack);
            }

            stack.Push(item);
        }
        
        public void Close<T>(StateOptions options = StateOptions.ClosePreviousAndAddToStack, int layer = 0)
            where T : IState
        {
            if (options.HasFlag(StateOptions.LinkWithLastOnStack))
            {
                if (orderedStates.TryGetValue(layer, out Stack<IState> stack) && stack.Count > 0)
                {
                    IState statable = stack.Peek();
                    if (linkedStates.TryGetValue(statable, out HashSet<IState> linked))
                    {
                        foreach (IState linkedState in linked)
                        {
                            if (linkedState.GetType() == typeof(T))
                            {
                                linkedState.Close();
                                linked.Remove(linkedState);
                                statable.OnLinkedStateClosed();
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
                if (orderedStates.TryGetValue(layer, out Stack<IState> stack) && stack.Count > 0)
                {
                    IState statable = stack.Peek();
                    if (linkedStates.TryGetValue(statable, out HashSet<IState> linked))
                    {
                        foreach (IState linkedState in linked)
                        {
                            StateAttribute attribute = linkedState.GetType().GetCustomAttribute<StateAttribute>();
                            if (attribute != null && attribute.Key == key)
                            {
                                linkedState.Close();
                                linked.Remove(linkedState);
                                statable.OnLinkedStateClosed();
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void ClearStack()
        {
            foreach ((int layer, var stack) in orderedStates)
            {
                foreach (var state in stack)
                {
                    state.Close();
                }
                
                stack.Clear();
            }
            
            orderedStates.Clear();
        }

        private void Clear(int sourceLayer)
        {
            foreach ((int layer, var set) in nonOrderedStates)
            {
                if (layer >= sourceLayer)
                {
                    set.Clear();
                }
            }
            
            foreach ((int layer, var stack) in orderedStates)
            {
                if (layer >= sourceLayer && stack.TryPop(out IState statable))
                {
                    statable.Close();
                    if (linkedStates.TryGetValue(statable, out HashSet<IState> set))
                    {
                        set.Clear();
                    }
                }
            }
        }


        private void Close(int sourceLayer)
        {
            foreach ((int layer, var set) in nonOrderedStates)
            {
                if (layer >= sourceLayer)
                {
                    foreach (IState state in set)
                    {
                        state.Close();
                    }
                }
            }
            
            foreach ((int layer, var stack) in orderedStates)
            {
                if (layer >= sourceLayer && stack.TryPeek(out IState statable))
                {
                    statable.Close();
                    if (linkedStates.TryGetValue(statable, out HashSet<IState> set))
                    {
                        foreach (IState linkedState in set)
                        {
                            linkedState.Close();
                        }
                    }
                }
            }
        }

        public void OpenLast(int layer = 0)
        {
            if (orderedStates.TryGetValue(layer, out Stack<IState> stack) && stack.Count > 0)
            {
                IState state = stack.Peek();
                state.Open();
                if (linkedStates.TryGetValue(state, out HashSet<IState> set))
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
            if (orderedStates.TryGetValue(layer, out var stack) && stack.Count > 0)
            {
                IState state = stack.Pop();
                state.OnReturn();
                state.Close();

                if (linkedStates.TryGetValue(state, out HashSet<IState> set))
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