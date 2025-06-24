using System;

namespace Modules.Tutorial
{
    [Serializable]
    public struct CommandTutorialHandle : ITutorialHandle
    {
        public string commandName;
    }
}