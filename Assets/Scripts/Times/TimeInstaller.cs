using JetBrains.Annotations;
using Modules.Data;
using VContainer;
using VContainer.Unity;

namespace Modules.Times
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
            builder.Register<TimeModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf()
                .WithParameter(resolver => resolver.Resolve<DataManager>().Load(TimeConstants.TIME_DATA_SAVE_KEY, new TimeData()));
        }
    }
}