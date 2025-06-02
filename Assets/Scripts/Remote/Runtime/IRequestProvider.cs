using Google.Apis.Services;

namespace Modules.Remote
{
    public interface IRequestProvider
    {
        void Request(BaseClientService.Initializer initializer);
    }
}