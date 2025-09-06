using UnityEngine;

namespace Modules.Data
{
    public enum DataType
    {
        None           = 0,
        PlayerPrefs    = 1,
        PersistentData = 2
    }
    
    [CreateAssetMenu(menuName = "Settings/" + nameof(DataSettings), fileName = nameof(DataSettings))]
    public class DataSettings : ScriptableObject
    {
        [field: SerializeField] public DataType DataType { get; private set; }
        [field: SerializeField] public SerializationSettings SerializationSettings { get; private set; }
    }
}