using System;
using Newtonsoft.Json;

namespace Modules.Data
{
    [Serializable]
    public sealed class SerializationSettings
    {
        public JsonType jsonType;
        public EncodingType encodingType;
        
        public Formatting formatting;
        public TypeNameHandling typeNameHandling;
    }
}