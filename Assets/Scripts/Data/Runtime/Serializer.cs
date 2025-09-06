using System;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Modules.Data
{
    public static class Serializer
    {
        public static string Serialize(object data, SerializationSettings serializationSettings)
        {
            string json = serializationSettings.jsonType switch 
            {
                JsonType.Newtonsoft => JsonConvert.SerializeObject(data, serializationSettings.formatting, new JsonSerializerSettings()
                {
                    TypeNameHandling = serializationSettings.typeNameHandling
                }),
                JsonType.Unity => JsonUtility.ToJson(data),
                _ => throw new ArgumentOutOfRangeException()
            };

            return serializationSettings.encodingType switch
            {
                EncodingType.Unicode => Convert.ToBase64String(Encoding.Unicode.GetBytes(json)),
                EncodingType.UTF8 => Convert.ToBase64String(Encoding.UTF8.GetBytes(json)),
                _ => json
            };
        }
        
        public static T Deserialize<T>(string json, SerializationSettings serializationSettings)
        {
            json = serializationSettings.encodingType switch 
            {
                EncodingType.Unicode => Encoding.Unicode.GetString(Convert.FromBase64String(json)),
                EncodingType.UTF8 => Encoding.UTF8.GetString(Convert.FromBase64String(json)),
                _ => json
            };
            
            return serializationSettings.jsonType switch 
            {
                JsonType.Newtonsoft => (T)JsonConvert.DeserializeObject(json, typeof(T), new JsonSerializerSettings()
                {
                    TypeNameHandling = serializationSettings.typeNameHandling,
                    Formatting = serializationSettings.formatting
                }),
                JsonType.Unity => (T)JsonUtility.FromJson(json, typeof(T)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}