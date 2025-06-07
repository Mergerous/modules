using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Architecture.Components;
using Modules.Architecture.Interfaces;
using Modules.Architecture.Managers;
using Modules.Common.Extensions;
using UnityEngine;

namespace Modules.Architecture.Behaviours
{
    public sealed class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private string key;
        [SerializeReference] private List<IComponent> components = new();
        [SerializeField] private Entity[] children;

        public string Key => key;

        private Dictionary<Type, IComponent> componentsCache;

        public Dictionary<Type, IComponent> Components
            => componentsCache ??= components.ToDictionary(component => component.GetType(), component => component);

        private void Start()
        {
            SystemsManager.Instance.Register(this);
        }

        private void OnDestroy()
        {
            SystemsManager.Instance.Unregister(this);
        }

        public IEnumerable<Behaviour> Find(Queue<string> path)
        {
            if (path.TryPeek(out string key) && key == Key)
            {
                if (path.Count > 1)
                {
                    path.Dequeue();
                    foreach (var child in children)
                    {
                        var result = child.Find(path);
                        foreach (var VARIABLE in result)
                        {
                            yield return VARIABLE;
                        }
                    }
                }
                
                yield return this;
            }
        }
        
        public Entity this[string key] => children.FirstOrDefault(child => child.Key == key);

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }
        
        public bool TryGetBaseComponent<T>(out T result)
        {
            if (Components.TryGetValue(typeof(T), out IComponent component))
            {
                result = (T)component;
                return result != null;
            }

            result = default;
            return false;
        }
    }
}