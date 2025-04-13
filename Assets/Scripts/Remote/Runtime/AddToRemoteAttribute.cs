using System;

namespace Modules.Remote
{
    public sealed class AddToRemoteAttribute : Attribute
    {
        public readonly string searchName;

        public AddToRemoteAttribute(string searchName)
        {
            this.searchName = searchName;
        }
    }
}
