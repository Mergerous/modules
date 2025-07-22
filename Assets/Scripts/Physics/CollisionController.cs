using System;
using UnityEngine;

namespace Modules.Physics
{
    [RequireComponent(typeof(Collider))]

    public class CollisionController : MonoBehaviour
    {
        private event Action<CollisionData> _collisionCallback;
        private event Action<CollisionData> _collisionExitCallback;

        #region Unity

        #endregion

        #region Public


        public CollisionController SetCollisionCallback(Action<CollisionData> collisionCallback)
        {
            _collisionCallback = collisionCallback;
            return this;
        }

        public CollisionController SetCollisionExitCallback(Action<CollisionData> collisionExitCallback)
        {
            _collisionExitCallback = collisionExitCallback;
            return this;
        }

        public CollisionController RemoveCollisionCallbacks()
        {
            _collisionCallback = default;
            _collisionExitCallback = default;
            return this;
        }

        #endregion

        #region Private

        private void OnCollisionEnter(Collision collision)
        {
            _collisionCallback?.Invoke(new CollisionData()
            {
                Self = transform,
                Another = collision.transform,
                Collision = collision
            });
        }

        private void OnCollisionExit(Collision collision)
        {
            _collisionExitCallback?.Invoke(new CollisionData()
            {
                Self = transform,
                Another = collision.transform,
                Collision = collision,
            });
        }

        #endregion
    }

    public class CollisionData
    {
        public Transform Self;
        public Transform Another;
        public Collision Collision;
    }
}