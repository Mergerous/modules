using System;
using UnityEngine;

namespace Modules.UI.Views.States
{
    [Serializable]
    public sealed class GameObjectStateComponent : StateComponent
    {
        [SerializeField] private bool isActive;
        [SerializeField] private GameObject gameObject;
        public override void Apply()
        {
            gameObject.SetActive(isActive);
        }
    }
}
