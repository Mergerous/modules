using UnityEngine;

namespace Modules.Data
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(DataSettings), fileName = nameof(DataSettings))]
    public class DataSettings : ScriptableObject
    {
        [field: SerializeField] public JsonType JsonType { get; private set; } = JsonType.Unity;
        [field: SerializeField] public EncodingType EncodingType { get; private set; } = EncodingType.UTF8;
        [field: SerializeField] public bool LogSaveEvent { get; private set; } = true;
    }
}