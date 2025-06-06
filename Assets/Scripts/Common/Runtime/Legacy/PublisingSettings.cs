using UnityEngine;

namespace Modules.Common
{
    [CreateAssetMenu(fileName = nameof(PublishingSettings), menuName = "Settings/" + nameof(PublishingSettings))]
    public class PublishingSettings : ScriptableObject
    {
        public string companyName = "Komboocha Games";
        [PasswordField] public string password;
    }
}
