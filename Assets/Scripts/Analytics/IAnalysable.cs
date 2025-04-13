using System;

namespace Modules.Analytics
{
    public interface IAnalysable
    {
        public event Action<string, AnalyticsArguments> OnEventTracked;
    }
}
