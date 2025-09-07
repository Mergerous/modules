using System.Linq;
using UnityEngine;

namespace Modules.Data
{
    public sealed class DumpAsset : ScriptableObject
    {
        [SerializeField, TextArea(30, 100)] private string text;
        [SerializeReference] private object[] data;
        
        public static DumpAsset Create(string text, SerializationSettings settings)
        {
            DumpAsset instance = CreateInstance<DumpAsset>();
            instance.text = text;
            instance.data = Serializer.Deserialize<object[]>(text, settings);
            return instance;
        }

        public T GetDataOrDefault<T>() => data.OfType<T>().FirstOrDefault();
    }
}