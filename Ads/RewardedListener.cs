using System;
using AppodealStack.Monetization.Common;


namespace Modules.Ads
{
    public class RewardedListener : IRewardedVideoAdListener
    {
        private event Action _rewardedVideoCallback;

        public void SetRewardVideoFinishCallback(Action callback)
        {
           _rewardedVideoCallback = callback; 
        }

        public void OnRewardedVideoLoaded(bool isPrecache)
        {
            
        }

        public void OnRewardedVideoFailedToLoad()
        {
            
        }

        public void OnRewardedVideoShowFailed()
        {
            
        }

        public void OnRewardedVideoShown()
        {
            // _rewardedVideoCallback?.Invoke();
        }

        public void OnRewardedVideoFinished(double amount, string currency)
        {
            _rewardedVideoCallback?.Invoke();
        }

        public void OnRewardedVideoClosed(bool finished)
        {
            
        }

        public void OnRewardedVideoExpired()
        {
            
        }

        public void OnRewardedVideoClicked()
        {
            
        }
    }
}
