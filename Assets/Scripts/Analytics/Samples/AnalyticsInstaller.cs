using System;
using VContainer;
using VContainer.Unity;

namespace Modules.Analytics
{
    [Serializable]
    public sealed class AnalyticsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<IAnalyticsService, FirebaseAnalyticsManager>(Lifetime.Singleton);
        }
    }
}