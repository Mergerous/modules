using System;
using UnityEngine;

namespace Modules.Tutorial
{
    [Serializable]
    public sealed class TutorialStep
    {
        [SerializeReference] public ITutorialHandle handle;
        [SerializeReference] public ITutorialSupport[] supports;
    }
}