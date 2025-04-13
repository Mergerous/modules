using System;

namespace Modules.States
{
    public class StateAttribute : Attribute
    {
        public string Key { get; }

        public StateAttribute(string key)
        {
            Key = key;
        }
    }
}