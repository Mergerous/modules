using System;
using Modules.Remote;
using UnityEngine;

namespace Settings
{
    [Serializable, AddToRemote("remote")]
    public sealed class SettingsRemoteInfo
    {
        [SerializeReference] public ISettingsContent[] contents;
    }
}
