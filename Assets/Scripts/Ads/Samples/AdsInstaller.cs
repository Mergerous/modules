using System;
using Modules.Ads;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ads.Samples
{
    [Serializable]
    public sealed class AdsInstaller : IInstaller
    {
        [SerializeField] private AdsSettings adsSettings;
        
        public void Install(IContainerBuilder builder)
        {
            builder.Register<AdsManager>(Lifetime.Singleton).WithParameter(adsSettings);
        }
    }
}