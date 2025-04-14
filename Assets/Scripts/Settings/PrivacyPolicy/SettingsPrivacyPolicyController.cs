using JetBrains.Annotations;
using Settings.Core;
using UnityEngine;

namespace Settings.PrivacyPolicy
{
    [UsedImplicitly]
    public sealed class SettingsPrivacyPolicyController
    {
        private readonly IPrivacyPolicyContent privacyPolicyContent;

        public SettingsPrivacyPolicyController(IPrivacyPolicyContent privacyPolicyContent)
        {
            this.privacyPolicyContent = privacyPolicyContent;
        }

        public void OpenPrivacyPolicy()
        {
            Application.OpenURL(privacyPolicyContent.PrivacyPolicyUrl);
        }
    }
}
