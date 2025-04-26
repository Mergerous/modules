using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.Core
{
    [Serializable]
    public sealed class SettingsData
    {
        [SerializeReference] public List<ISettingsData> data = new();
    }
}