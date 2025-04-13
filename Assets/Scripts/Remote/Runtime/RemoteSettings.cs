using UnityEngine;

namespace Modules.Remote
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(RemoteSettings), fileName = nameof(RemoteSettings))]
    public class RemoteSettings : ScriptableObject
    {
        [field: SerializeField] public bool ShouldFetchOnStart { get; private set; }
    }
}
