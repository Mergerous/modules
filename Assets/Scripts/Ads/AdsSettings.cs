using AppodealStack.Monetization.Common;
using UnityEngine;

namespace Modules.Ads
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(AdsSettings), fileName = nameof(AdsSettings))]
    public class AdsSettings : ScriptableObject
    {
        [field: SerializeField] public bool Testing { get; private set; }
        
        [field: SerializeField] public bool MuteVideosIfCallsMuted { get; private set; }
        
        [field: SerializeField] public AdType AdType { get; private set; } = AdType.Interstitial 
                                                                             | AdType.Banner 
                                                                             | AdType.Rewarded 
                                                                             | AdType.Mrec;
        [field: SerializeField] public AppodealLogLevel LogLevel { get; private set; }
        [field: SerializeField, TextArea] public string AppKey { get; private set; }

    }
}