using System;
using UnityEngine;

namespace Settings.Core
{
    [Serializable]
    public class SettingsData : ICloneable
    {
        public bool isSoundsEnabled = true;
        public bool isMusicEnabled = true;
        public bool isVibrationsEnabled = true;
        public SystemLanguage systemLanguage;
        public object Clone() => MemberwiseClone();
    }
}