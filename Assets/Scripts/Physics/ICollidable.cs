using System;

namespace Modules.Physics
{
    public interface ICollidable
    {
#if MODULES_RX
        public UniRx.ReactiveCommand<CollisionData> Collide { get; }
#else
        event Action<CollisionData> Collide;
#endif
    }
}