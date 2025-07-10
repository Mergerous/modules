using System;

namespace Modules.Tutorial
{
    [Serializable]
    public sealed class PointerTutorialSupport : ITutorialSupport
    {
        public string anchorKey;
        public string viewKey;
        public string pointerKey;
    }
}