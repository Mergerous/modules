using System;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Modules.Ads.Settings;

namespace Modules.Ads.Managers
{
    public enum BannerType
    {
        None = 0,
        Bottom = 1 << 1,
        Top = 1 << 2,
        Left = 1 << 3,
        Right = 1 << 4
    }
    
    public class AdsManager : IDisposable
    {
        private readonly AdsSettings _adsSettings;
        
        public AdsManager(AdsSettings adsSettings)
        {
            _adsSettings = adsSettings;
            Appodeal.SetLogLevel(_adsSettings.LogLevel);
            Appodeal.SetTesting(_adsSettings.Testing);
            Appodeal.MuteVideosIfCallsMuted(_adsSettings.MuteVideosIfCallsMuted);
            Appodeal.Initialize(_adsSettings.AppKey, (int)adsSettings.AdType);
        }

        ~AdsManager()
        {
            ReleaseUnmanagedResources();
        }

        public RewardedListener ShowRewardedVideo()
        {
            RewardedListener listener = new RewardedListener();
            Appodeal.SetRewardedVideoCallbacks(listener);
            
            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
            {
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
            }

            return listener;
        }

        public InterstitialListener ShowInterstitial()
        {
            InterstitialListener listener = new InterstitialListener();
            Appodeal.SetInterstitialCallbacks(listener);
            
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
            {
                Appodeal.Show(AppodealShowStyle.Interstitial);
            }

            return listener;
        }

        public void ShowBanner(BannerType bannerType = BannerType.Bottom)
        {
            BannerListener listener = new BannerListener();
            Appodeal.SetBannerCallbacks(listener);
            
            if (Appodeal.IsLoaded(AppodealAdType.Banner))
            {
                Appodeal.Show((int)bannerType);
            }
        }

        public void HideBanner()
        {
            if (Appodeal.IsLoaded(AppodealAdType.Banner))
            {
                Appodeal.Hide(AppodealAdType.Banner);
            }
        }

        private void ReleaseUnmanagedResources()
        {
            Appodeal.Destroy(AppodealAdType.Banner);
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}
    