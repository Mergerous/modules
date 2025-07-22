using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Physics
{
    
    [RequireComponent(typeof(Collider2D))]
    public class CollisionController2D : MonoBehaviour
    {

        private event Action<CollisionData> _colCallback;

        #region Unity

        private void OnCollisionEnter2D(Collision2D collision)
        {

            _colCallback?.Invoke(new CollisionData()
            {
                Self = transform,
                Another = collision.transform
            });
        }

        #endregion

        #region Public

        public void SetColCallback(Action<CollisionData> callback)
        {
            _colCallback = callback;
        }

        #endregion

        #region Private

        #endregion
    }
}