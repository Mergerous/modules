using System;
using Modules.Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Modules.Tutorial
{
    [Serializable]
    public sealed class TutorialInstaller : IInstaller
    {
        [SerializeField] private TutorialSettings tutorialSettings;
        public void Install(IContainerBuilder builder)
        {
            // Models
            //
            builder.Register<TutorialModel>(Lifetime.Singleton)
                .WithParameter(tutorialSettings)
                .WithParameter(resolver => resolver.Resolve<DataManager>().Load(TutorialConstants.TUTORIAL_DATA_SAVE_KEY, new TutorialData()));
            
            // Core
            //
            builder.RegisterEntryPoint<TutorialManager>();
            builder.Register<ITutorialSupporter, AnchorTutorialSupporter>(Lifetime.Singleton);
            builder.Register<ITutorialHandler, CommandTutorialHandler>(Lifetime.Singleton);
            builder.Register<CommandTutorialBlackboard>(Lifetime.Singleton);
        }
    }
}