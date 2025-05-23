using JetBrains.Annotations;
using UnityEngine;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class PrivacyPolicySettingsProcessor : IButtonSettingsProcessor
    {
        public void Process(ISettingsItemModel model)
        {
            if (model is PrivacyPolicySettingsModel privacyPolicySettingsModel)
            {
                Application.OpenURL(privacyPolicySettingsModel.Url);
            }
        }
    }
}
