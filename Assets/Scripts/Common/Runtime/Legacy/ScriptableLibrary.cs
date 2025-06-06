using System.Collections.Generic;
using UnityEngine;

namespace Modules.Common
{
    [CreateAssetMenu(fileName = nameof(ScriptableLibrary), menuName = "Settings/" + nameof(ScriptableLibrary))]
    public class ScriptableLibrary : ScriptableObject
    {
        [SerializeReference] public List<ScriptableObject> variants;
    }
}
