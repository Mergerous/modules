using System;

namespace Modules.Views
{
    public interface IViewHandle : IDisposable
    {
        void Initialize();
        View View { get; }
    }
}