using JetBrains.Annotations;
using Modules.Data;
using VContainer;
using VContainer.Unity;

namespace Modules.Time
{
    [UsedImplicitly]
    public sealed class TimeInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            // Entry
            //
            builder.RegisterEntryPoint<TimeManager>();
            
            // Debugging
            //
            builder.RegisterEntryPoint<TimeDebug>();
            
            // Models
            //
            builder.Register<TimeManager>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Data
            //
            builder.RegisterFactory<TimeData>(container => 
                () => container.Resolve<DataManager>().Load(TimeConstants.TIME_DATA_SAVE_KEY, new TimeData()), Lifetime.Scoped);
        }
    }
}