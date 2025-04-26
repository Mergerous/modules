using JetBrains.Annotations;
using Settings;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class PrivacyPolicySettingsModel : ISettingsItemModel, IButtonContent
    {
        private readonly PrivacyPolicySettingsContent content;
        
        public string Url => content.url;

        public PrivacyPolicySettingsModel(PrivacyPolicySettingsContent content)
        {
            this.content = content;
        }
    }
}