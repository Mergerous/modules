using System;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

namespace Modules.Ads
{
    public class AdsManager : IDisposable
    {
        private readonly AdsSettings adsSettings;

        public bool IsInitialized => Appodeal.IsInitialized((int)adsSettings.AdType);
        
        public AdsManager(AdsSettings adsSettings)
        {
            this.adsSettings = adsSettings;
            Appodeal.SetLogLevel(this.adsSettings.LogLevel);
            Appodeal.SetTesting(this.adsSettings.Testing);
            Appodeal.MuteVideosIfCallsMuted(this.adsSettings.MuteVideosIfCallsMuted);
            Appodeal.Initialize(this.adsSettings.AppKey, (int)adsSettings.AdType);
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
    