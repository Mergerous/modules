using JetBrains.Annotations;

namespace Modules.Tutorial
{
    [UsedImplicitly]
    public sealed class TutorialModel
    {
        public readonly TutorialSettings config;
        public readonly TutorialData data;

        public TutorialModel(TutorialSettings config, TutorialData data)
        {
            this.config = config;
            this.data = data;
        }
    }
}