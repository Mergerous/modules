using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Modules.Architecture.Behaviours;
using Modules.Architecture.Components;
using Units;
using UnityEngine;

namespace Modules.Architecture.Overrides
{
    [Serializable]
    public sealed class EntityOverride
    {
        public string key;
        public EntityOverride[] children;
        [SerializeReference] public List<IOverride> overrides;
 
        public IEnumerable<(EntityOverride, Queue<string>)> GetChildren(Queue<string> path)
        {
            Queue<string> newPath = new Queue<string>(path);
            newPath.Enqueue(key);
            
            foreach (EntityOverride entityOverride in children)
            {
                foreach ((EntityOverride childrenOverride, Queue<string> childPath) in entityOverride.GetChildren(newPath))
                {
                    yield return (childrenOverride, childPath);
                }
            }
            
            yield return (this, newPath);
        }

        public void ApplyEntity(Entity e)
        {
            var stack = new Queue<string>();
            foreach ((EntityOverride eo, Queue<string> cs) in GetChildren(stack))
            {
                var ce = e.Find(cs);
                foreach (var c in ce)
                {
                    eo.Apply(c as Entity);
                }
            }
        }

        public EntityOverride this[string key] => children.First(child => child.key == key);

        public void Combine(EntityOverride entityOverride)
        {
            var stack1 = new Queue<string>();
            var stack2 = new Queue<string>();
            
            foreach ((EntityOverride child1, Queue<string> path1) in GetChildren(stack1))
            {
                var p1 = path1.Aggregate((a, b) => a + b);
                foreach ((EntityOverride child2, Queue<string> path2) in entityOverride.GetChildren(stack2))
                {
                    var p2 = path2.Aggregate((a, b) => a + b);
                    
                    if (p1 == p2)
                    {
                        for (int i = 0; i < child2.overrides.Count; i++)
                        {
                            bool hasOverride = false;
                            IOverride externalOverride = child2.overrides[i];

                            for (int j = 0; j < child1.overrides.Count; j++)
                            {
                                if (externalOverride.GetType() == child1.overrides[j].GetType())
                                {
                                    child1.overrides[j] = externalOverride;
                                    hasOverride = true;
                                    break;
                                }
                            }

                            if (!hasOverride)
                            {
                                child1.overrides.Add(externalOverride);
                            }
                        }
                    }
                }
            }
        }

        private void Apply(Entity entity)
        {
            foreach (var ov in overrides)
            {
                if (ov.GetType().GetCustomAttribute(typeof(OverrideAttribute)) is OverrideAttribute attribute)
                {
                    foreach ((Type type, IComponent comp) in entity.Components)
                    {
                        if (type == attribute.type)
                        {
                            ov.Apply(comp);
                        }
                    }
                }
            }
        }
    }
}