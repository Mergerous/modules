using System;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Modules.Data
{
    public static class Serializer
    {
        public static string Serialize(object data, JsonType jsonType, EncodingType encodingType)
        {
            string json = jsonType switch 
            {
                JsonType.Newtonsoft => JsonConvert.SerializeObject(data),
                JsonType.Unity => JsonUtility.ToJson(data),
                _ => throw new ArgumentOutOfRangeException()
            };

            return encodingType switch
            {
                EncodingType.Unicode => Convert.ToBase64String(Encoding.Unicode.GetBytes(json)),
                EncodingType.UTF8 => Convert.ToBase64String(Encoding.UTF8.GetBytes(json)),
                _ => json
            };
        }
        
        public static T Deserialize<T>(string json, JsonType jsonType, EncodingType encodingType)
        {
            json = encodingType switch 
            {
                EncodingType.Unicode => Encoding.Unicode.GetString(Convert.FromBase64String(json)),
                EncodingType.UTF8 => Encoding.UTF8.GetString(Convert.FromBase64String(json)),
                _ => json
            };
            
            return jsonType switch 
            {
                JsonType.Newtonsoft => (T)JsonConvert.DeserializeObject(json, typeof(T)),
                JsonType.Unity => (T)JsonUtility.FromJson(json, typeof(T)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}