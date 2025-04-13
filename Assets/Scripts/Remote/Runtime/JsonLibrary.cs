using System.Collections.Generic;
using UnityEngine;

namespace Modules.Remote
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(JsonLibrary), fileName = nameof(JsonLibrary))]
    public sealed class JsonLibrary : ScriptableObject
    {
        [SerializeReference] public List<object> Defaults;
    }
}