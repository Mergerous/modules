using System;
using Modules.Remote;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu]
    public sealed class SettingsRemoteInfo : SerializedScriptableObject
    {
        [SerializeReference] public ISettingsContent[] contents;
    }
}
