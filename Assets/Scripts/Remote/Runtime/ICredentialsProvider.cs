using Google.Apis.Sheets.v4;

namespace Modules.Remote
{
    public interface ICredentialsProvider
    {
        SheetsService GetSheetsService();
    }
}