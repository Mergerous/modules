using System;
using AppodealStack.Monetization.Common;

namespace Modules.Ads
{
    public class RewardedListener : IRewardedVideoAdListener
    {
        private event Action rewardedVideoCallback;
        private event Action rewardVideoFailCallback;

        public void SetRewardVideoFinishCallback(Action callback)
        {
           rewardedVideoCallback = callback; 
        }
        
        public void SetRewardVideoFailCallback(Action callback)
        {
            rewardVideoFailCallback = callback; 
        }

        public void OnRewardedVideoLoaded(bool isPrecache)
        {
            
        }

        public void OnRewardedVideoFailedToLoad()
        {
            
        }

        public void OnRewardedVideoShowFailed()
        {
            rewardVideoFailCallback?.Invoke();
        }

        public void OnRewardedVideoShown()
        {
            // _rewardedVideoCallback?.Invoke();
        }

        public void OnRewardedVideoFinished(double amount, string currency)
        {
            rewardedVideoCallback?.Invoke();
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
