using System.Collections.Generic;
using UnityEngine;

namespace Modules.Common
{
    [CreateAssetMenu(fileName = nameof(KeysLibrary), menuName = "Settings/" + nameof(KeysLibrary))]
    public sealed class KeysLibrary : ScriptableObject
    {
        [SerializeField] private KeysSettings[] settings;

        public IEnumerable<KeysSettings> Settings => settings;
    }
}
