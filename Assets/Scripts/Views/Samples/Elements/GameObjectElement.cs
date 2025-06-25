using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class GameObjectElement : Element
    {
        [field: SerializeField] public GameObject GameObject { get; private set; }
    }
}
