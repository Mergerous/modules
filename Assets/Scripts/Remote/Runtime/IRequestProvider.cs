using Google.Apis.Sheets.v4;

namespace Modules.Remote
{
    public interface IRequestProvider
    {
        void Request(SheetsService service);
    }
}