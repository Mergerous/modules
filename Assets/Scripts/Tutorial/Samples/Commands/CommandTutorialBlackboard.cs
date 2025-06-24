using JetBrains.Annotations;
using R3;

namespace Modules.Tutorial
{
    [UsedImplicitly]
    public sealed class CommandTutorialBlackboard
    {
        private readonly ReactiveCommand<string> command = new();
        
        public Observable<string> CommandObservable => command;

        public void Execute(string key)
        {
            command.Execute(key);
        }
    }
}