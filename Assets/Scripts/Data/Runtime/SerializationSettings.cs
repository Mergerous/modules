using System;
using Newtonsoft.Json;

namespace Modules.Data
{
    [Serializable]
    public struct SerializationSettings
    {
        public JsonType jsonType;
        public EncodingType encodingType;
        
        public Formatting formatting;
        public TypeNameHandling typeNameHandling;
    }
}