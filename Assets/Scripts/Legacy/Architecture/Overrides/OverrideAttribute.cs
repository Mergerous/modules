using System;

namespace Units
{
    public class OverrideAttribute : Attribute
    {
        public Type type;

        public OverrideAttribute(Type type)
        {
            this.type = type;
        }
    }
}
