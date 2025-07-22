using System;
using System.Collections.Generic;
#if MODULES_RX
using UniRx;
#endif

namespace Modules.Physics
{

    public class CollisionManager
    {
        private readonly List<CollisionFilter> _filters = new List<CollisionFilter>();
#if MODULES_RX
        private readonly CompositeDisposable _disposables = new UniRx.CompositeDisposable();
#endif
        
        public void Register(ICollidable collidable)
        {
#if MODULES_RX
            collidable.Collide.Subscribe(OnCollided).AddTo(_disposables);
#else
            collidable.Collide += OnCollided;
#endif
        }

        private void OnCollided(CollisionData collision)
        {
            foreach (var filter in _filters)
            {
                if (collision.Self.TryGetComponent(filter.First, out var component1)
                    && collision.Another.TryGetComponent(filter.Second, out var component2))
                {
                    filter.Callback?.DynamicInvoke(component1, component2);
                }
            }
        }

        public void AddFilter(CollisionFilter filter)
        {
            _filters.Add(filter);
        }

        public void RemoveFilter(CollisionFilter filter)
        {
            if (_filters.Contains(filter))
            {
                _filters.Remove(filter);
            }
        }
    }

    public class CollisionFilter
    {
        public Type First;
        public Type Second;
        public Delegate Callback;

        public void SetCallback<T, TT>(Action<T, TT> callback)
        {
            First = typeof(T);
            Second = typeof(TT);
            Callback = callback;
        }
    }

}