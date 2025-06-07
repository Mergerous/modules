using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Architecture.Interfaces;

namespace Modules.Architecture.Managers
{
    [UsedImplicitly]
    public sealed class SystemsManager
    {
        public static SystemsManager Instance { get; private set; }
        
        private readonly IEnumerable<ISystem> systems;
        private readonly List<IEntity> entities;

        public SystemsManager(IEnumerable<ISystem> systems)
        {
            this.systems = systems;
            entities = new List<IEntity>();
            Instance = this;
        }

        public void Register(IEntity entity)
        {
            entities.Add(entity);
            foreach (ISystem system in systems)
            {
                system.Register(entity);
            }
        }
        
        public void Unregister(IEntity entity)
        {
            entities.Remove(entity);
            foreach (ISystem system in systems)
            {
                system.Unregister(entity);
            }
        }
    }
}
