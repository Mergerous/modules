using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Modules.Data;
using VContainer.Unity;

namespace Modules.Tutorial
{
    [UsedImplicitly]
    public sealed class TutorialManager : IStartable, IDisposable
    {
        private readonly DataManager dataManager;
        private readonly TutorialModel tutorialModel;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly IEnumerable<ITutorialSupporter> supporters;
        private readonly IEnumerable<ITutorialHandler> handlers;

        public TutorialManager(DataManager dataManager, TutorialModel tutorialModel, IEnumerable<ITutorialSupporter> supporters, IEnumerable<ITutorialHandler> handlers)
        {
            this.dataManager = dataManager;
            this.tutorialModel = tutorialModel;
            this.supporters = supporters;
            this.handlers = handlers;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public async void Start()
        {
            while (tutorialModel.data.stepIndex < tutorialModel.config.Steps.Count)
            {
                await Process(tutorialModel.config.Steps[tutorialModel.data.stepIndex], cancellationTokenSource.Token);
                tutorialModel.data.stepIndex++;
                dataManager.Save(TutorialConstants.TUTORIAL_DATA_SAVE_KEY, tutorialModel.data);
                
                if (cancellationTokenSource.Token.IsCancellationRequested)
                {
                    return;
                }
            }
        }
        
        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        private async Task Process(TutorialStep tutorialStep, CancellationToken cancellationToken)
        {
            foreach (ITutorialSupporter supporter in supporters)
            {
                supporter.Support(tutorialStep.support);
            }
            
            foreach (ITutorialHandler handler in handlers)
            {
                await handler.Handle(tutorialStep.handle, cancellationToken);
            }

            foreach (ITutorialSupporter supporter in supporters)
            {
                supporter.Dispose();
            }
        }
    }
}