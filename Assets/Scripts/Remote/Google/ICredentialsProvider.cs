using Google.Apis.Services;

namespace Modules.Remote
{
    public interface ICredentialsProvider
    {
        BaseClientService.Initializer GetClientService();
    }
}