using UnityEngine;

namespace Settings.Core
{
    public interface ILanguageContent
    {
        void SetLanguage(SystemLanguage language, bool shouldSave);

        SystemLanguage GetLanguage();
    }
}
