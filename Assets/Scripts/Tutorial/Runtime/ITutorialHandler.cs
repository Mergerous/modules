using System.Threading;
using System.Threading.Tasks;

namespace Modules.Tutorial
{
    public interface ITutorialHandler
    {
        Task Handle(ITutorialHandle handle, CancellationToken cancellationToken);
    }
}