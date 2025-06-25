using System;

namespace Modules.Scopes
{
    public sealed class ScopedByAttribute : Attribute
    {
        public readonly Type type;

        public ScopedByAttribute(Type type)
        {
            this.type = type;
        }
    }
}