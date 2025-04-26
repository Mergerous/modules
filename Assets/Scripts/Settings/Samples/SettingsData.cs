using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Settings
{
    [Serializable]
    public sealed class SettingsData
    {
        [SerializeReference] public List<ISettingsData> data = new();
    }
}