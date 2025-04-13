using System;

namespace Modules.UI.Views
{
    public sealed class GetViewAttribute : Attribute
    {
        public readonly string key;

        public GetViewAttribute(string key)
        {
            this.key = key;
        }
    }
}