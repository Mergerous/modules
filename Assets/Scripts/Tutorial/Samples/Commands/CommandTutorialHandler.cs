using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using R3;

namespace Modules.Tutorial
{
    [UsedImplicitly]
    public sealed class CommandTutorialHandler : ITutorialHandler
    {
        private readonly CommandTutorialBlackboard blackboard;

        public CommandTutorialHandler(CommandTutorialBlackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public async Task Handle(ITutorialHandle handle, CancellationToken cancellationToken)
        {
            if (handle is CommandTutorialHandle commandTutorialHandle)
            {
                await blackboard.CommandObservable
                    .Where(key => commandTutorialHandle.commandName == key)
                    .FirstAsync(cancellationToken);
            }
        }
    }
}