using System.Collections.Generic;

namespace Modules.Analytics
{
    public interface IAnalyticsService
    {
        void TrackEvent(string eventName);
        void TrackEvent(string eventName, string parameterName, object obj);
        void TrackEvent(string eventName, string parameterName, string json);
        void TrackEvent(string eventName, IReadOnlyDictionary<string, object> dict);
    }
}