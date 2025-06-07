using System;
using System.Collections.Generic;
using Modules.Architecture.Interfaces;

namespace Modules.Architecture.Systems
{
    public abstract class BaseSystem<TArchetype> : ISystem
    {
        protected readonly List<TArchetype> archetypes;
        protected readonly Dictionary<object, IDisposable> disposables;
        
        protected BaseSystem()
        {
            archetypes = new List<TArchetype>();
            disposables = new Dictionary<object, IDisposable>();
        }

        public abstract void Register(IEntity entity);

        public abstract void Unregister(IEntity entity);
    }


    public abstract class System<T> : BaseSystem<Archetype<T>>
    {
        public sealed override void Register(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component))
            {
                Archetype<T> archetype = new Archetype<T>(component);
                archetypes.Add(archetype);
                OnRegistered(component);
            }
        }

        public sealed override void Unregister(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component))
            {
                Archetype<T> archetype = archetypes.Find(subsystem =>
                {
                    subsystem.Deconstruct(out T item1);
                    return item1.Equals(component);
                });

                archetypes.Remove(archetype);
                OnUnregistered(component);
            }
        }

        protected virtual void OnRegistered(T component)
        {
        
        }

        protected virtual void OnUnregistered(T component)
        {
        
        }
    }
    

    public abstract class System<T, TT> : BaseSystem<Archetype<T, TT>>
    {
        public sealed override void Register(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component1) 
                && entity.TryGetBaseComponent(out TT component2))
            {
                Archetype<T, TT> archetype = new Archetype<T, TT>(component1, component2);
                archetypes.Add(archetype);
                OnRegistered(component1, component2);
            }
        }

        public sealed override void Unregister(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component1)
                && entity.TryGetBaseComponent(out TT component2))
            {
                foreach (Archetype<T, TT> archetype in archetypes)
                {
                    archetype.Deconstruct(out T item1, out TT item2);
                    if (component1.Equals(item1) && component2.Equals(item2))
                    {
                        OnUnregistered(component1, component2);
                        archetypes.Remove(archetype);
                        break;
                    }
                }
            }
        }

        protected virtual void OnRegistered(T component1, TT component2)
        {
        
        }

        protected virtual void OnUnregistered(T component1, TT component2)
        {
        
        }
    }

    public abstract class System<T, TT, TTT> : BaseSystem<Archetype<T, TT, TTT>>
    {
        public sealed override void Register(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component1) 
                && entity.TryGetBaseComponent(out TT component2)
                && entity.TryGetBaseComponent(out TTT component3))
            {
                Archetype<T, TT, TTT> archetype = new Archetype<T, TT, TTT>(component1, component2, component3);
                archetypes.Add(archetype);
                OnRegistered(component1, component2, component3);
            }
        }
    
        public sealed override void Unregister(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component1) 
                && entity.TryGetBaseComponent(out TT component2)
                && entity.TryGetBaseComponent(out TTT component3))
            {

                foreach (Archetype<T, TT, TTT> archetype in archetypes)
                {
                    archetype.Deconstruct(out T item1, out TT item2, out TTT item3);
                    if (item1.Equals(component1) && item2.Equals(component2) && item3.Equals(component3))
                    {
                        OnUnregistered(component1, component2, component3);
                        archetypes.Remove(archetype);
                        break;
                    }
                }
            }
        }
        
        protected virtual void OnRegistered(T component1, TT mergeTileComponent, TTT component3)
        {
        
        }

        protected virtual void OnUnregistered(T component1, TT mergeTileComponent, TTT component3)
        {
        
        }
    }
    
    public abstract class System<T, TT, TTT, TTTT> : BaseSystem<Archetype<T, TT, TTT, TTTT>>
    {
        public sealed override void Register(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component1) 
                && entity.TryGetBaseComponent(out TT component2)
                && entity.TryGetBaseComponent(out TTT component3)
                && entity.TryGetBaseComponent(out TTTT component4))
            {
                Archetype<T, TT, TTT, TTTT> archetype = new Archetype<T, TT, TTT, TTTT>(component1, component2, component3, component4);
                archetypes.Add(archetype);
                OnRegistered(component1, component2, component3, component4);
            }
        }
    
        public sealed override void Unregister(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component1) 
                && entity.TryGetBaseComponent(out TT component2)
                && entity.TryGetBaseComponent(out TTT component3)
                && entity.TryGetBaseComponent(out TTTT component4))
            {

                foreach (Archetype<T, TT, TTT, TTTT> archetype in archetypes)
                {
                    archetype.Deconstruct(out T item1, out TT item2, out TTT item3, out TTTT item4);
                    if (item1.Equals(component1) 
                        && item2.Equals(component2) 
                        && item3.Equals(component3) 
                        && item4.Equals(component4))
                    {
                        OnUnregistered(component1, component2, component3, component4);
                        archetypes.Remove(archetype);
                        break;
                    }
                }
            }
        }
        
        protected virtual void OnRegistered(T component1, TT component2, TTT component3, TTTT component4)
        {
        
        }

        protected virtual void OnUnregistered(T component1, TT component2, TTT component3, TTTT component4)
        {
        
        }
    }
    
    public abstract class System<T, T2, T3, T4, T5> : BaseSystem<Archetype<T, T2, T3, T4, T5>>
    {
        public sealed override void Register(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component1) 
                && entity.TryGetBaseComponent(out T2 component2)
                && entity.TryGetBaseComponent(out T3 component3)
                && entity.TryGetBaseComponent(out T4 component4)
                && entity.TryGetBaseComponent(out T5 component5))
            {
                Archetype<T, T2, T3, T4, T5> archetype = new Archetype<T, T2, T3, T4, T5>(component1, component2, component3, component4, component5);
                archetypes.Add(archetype);
                OnRegistered(component1, component2, component3, component4, component5);
            }
        }
    
        public sealed override void Unregister(IEntity entity)
        {
            if (entity.TryGetBaseComponent(out T component1) 
                && entity.TryGetBaseComponent(out T2 component2)
                && entity.TryGetBaseComponent(out T3 component3)
                && entity.TryGetBaseComponent(out T4 component4)
                && entity.TryGetBaseComponent(out T5 component5))
            {
                foreach (Archetype<T, T2, T3, T4, T5> archetype in archetypes)
                {
                    archetype.Deconstruct(out T item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5);
                    if (item1.Equals(component1)
                        && item2.Equals(component2)
                        && item3.Equals(component3)
                        && item4.Equals(component4)
                        && item5.Equals(component5))
                    {
                        OnUnregistered(component1, component2, component3, component4, component5);
                        archetypes.Remove(archetype);
                        break;
                    }
                }
            }
        }
        
        protected virtual void OnRegistered(T component1, T2 component2, T3 component3, T4 component4, T5 component5)
        {
        
        }

        protected virtual void OnUnregistered(T component1, T2 component2, T3 component3, T4 component4, T5 component5)
        {
        
        }
    }
}