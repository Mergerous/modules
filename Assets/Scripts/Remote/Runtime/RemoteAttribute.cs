using System;

namespace Modules.Remote
{
    public class GetFromRemoteAttribute : Attribute
    {
        public string Key { get; }
        public Type Type { get; }

        public GetFromRemoteAttribute(string key, Type type)
        {
            Key = key;
            Type = type;
        }
    }
}