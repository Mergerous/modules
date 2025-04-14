using JetBrains.Annotations;
using Settings.Core;
using UnityEngine;

namespace Settings.Localization
{
    [UsedImplicitly]
    public sealed class SettingsLocalizationController
    {
        private readonly ILanguageContent languageContent;

        public SettingsLocalizationController(ILanguageContent languageContent)
        {
            this.languageContent = languageContent;
        }

        public void SetLanguage(SystemLanguage language)
        {
            // LocalizationSettings.Instance.
            languageContent.SetLanguage(language, true);
        }

        public SystemLanguage GetLanguage()
        {
            return languageContent.GetLanguage();
        }
    }
}
