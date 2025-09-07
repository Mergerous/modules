using Newtonsoft.Json;
using UnityEngine;

namespace Modules.Data
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(DataSettings), fileName = nameof(DataSettings))]
    public class DataSettings : ScriptableObject
    {
        [field: SerializeField] public DataType DataType { get; private set; }
        [field: SerializeField] public DumpAsset DefaultDumpAsset { get; private set; }
        [field: SerializeField] public SerializationSettings SerializationSettings { get; private set; } = new()
        {
            encodingType = EncodingType.Unicode,
            formatting = Formatting.Indented,
            typeNameHandling = TypeNameHandling.Objects,
            jsonType = JsonType.Unity
        };
    }
}