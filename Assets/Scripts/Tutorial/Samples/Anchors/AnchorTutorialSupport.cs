using System;

namespace Modules.Tutorial
{
    [Serializable]
    public sealed class AnchorTutorialSupport : ITutorialSupport
    {
        public string anchorKey;
        public string viewKey;
    }
}