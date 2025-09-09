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
            return (T)Deserialize(json, typeof(T), serializationSettings);
        }
        
        public static object Deserialize(string json, Type type, SerializationSettings serializationSettings)
        {
            json = serializationSettings.encodingType switch 
            {
                EncodingType.Unicode => Encoding.Unicode.GetString(Convert.FromBase64String(json)),
                EncodingType.UTF8 => Encoding.UTF8.GetString(Convert.FromBase64String(json)),
                _ => json
            };
            
            return serializationSettings.jsonType switch 
            {
                JsonType.Newtonsoft => JsonConvert.DeserializeObject(json, type, new JsonSerializerSettings()
                {
                    TypeNameHandling = serializationSettings.typeNameHandling,
                    Formatting = serializationSettings.formatting
                }),
                JsonType.Unity => JsonUtility.FromJson(json, type),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}