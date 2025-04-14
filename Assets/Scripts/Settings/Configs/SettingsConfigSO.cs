using JetBrains.Annotations;
using UnityEngine;

namespace Settings.Configs
{
    [UsedImplicitly]
    public sealed class SettingsConfigSO : ScriptableObject
    {
        [field: SerializeField] public string PrivacyPolicyUrl { get; private set; }
        [field: SerializeField] public SystemLanguage[] Languages { get; private set; }
    }
}