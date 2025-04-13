using System;

namespace Modules.Ads
{
    [Flags]
    public enum AdType
    {
        None = 0,
        Interstitial = 1 << 0,
        Banner = 1 << 1,
        Rewarded = 1 << 2,
        Mrec = 1 << 3
    }
}