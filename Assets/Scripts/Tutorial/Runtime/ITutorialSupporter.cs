using System;

namespace Modules.Tutorial
{
    public interface ITutorialSupporter : IDisposable
    {
        void Support(ITutorialSupport support);
    }
}