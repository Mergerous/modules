using Data.Runtime;
using System;
using Modules.Data;
using VContainer;
using VContainer.Unity;

namespace Modules.Times
{
    [Serializable]
    public sealed class TimeInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            // Entry
            //
            builder
                .RegisterEntryPoint<TimeManager>()
                .WithParameter(resolver => resolver.Resolve<IDataService>(DataConstants.DATA_PLAYER_PREFS_KEY));
            
            // Debugging
            //
            builder.RegisterEntryPoint<TimeDebug>();
            
            // Models
            //
            builder.Register<TimeModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf()
                .WithParameter(resolver => resolver
                    .Resolve<IDataService>(DataConstants.DATA_PLAYER_PREFS_KEY)
                    .Load(TimeConstants.TIME_DATA_SAVE_KEY, new TimeData()));
        }
    }
}