using System.Collections.Generic;

namespace Modules.Analytics
{
    public struct AnalyticsArguments
    {
        public TrackEventType type;
        public object obj;
        public string json;
        public Dictionary<string, object> parameters;

        public AnalyticsArguments(TrackEventType type = TrackEventType.None)
        {
            this.type = type;
            obj = default;
            json = default;
            parameters = default;
        }
    }
}