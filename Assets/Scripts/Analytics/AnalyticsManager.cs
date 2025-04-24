using System.Collections.Generic;
using System.Linq;
using Firebase.Analytics;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Modules.Analytics
{
    [UsedImplicitly]
    public sealed class AnalyticsManager
    {
        public void TrackEvent(string eventName)
        {
            FirebaseAnalytics.LogEvent(eventName);
            Debug.Log($"LOG_EVENT {eventName}");
        }

        public void TrackEvent(string eventName, string parameterName, object obj)
        {
            TrackEvent(eventName, parameterName, JsonConvert.SerializeObject(obj));
        }

        public void TrackEvent(string eventName, string parameterName, string json)
        {
            FirebaseAnalytics.LogEvent(eventName, parameterName, json);
            Debug.Log($"LOG_EVENT {eventName} {json}");
        }

        public void TrackEvent(string eventName, Dictionary<string, object> dict)
        {
            FirebaseAnalytics.LogEvent(eventName, dict
                .Select(pair => new Parameter(pair.Key, JsonConvert.SerializeObject(pair.Value)))
                .ToArray());
            Debug.Log($"LOG_EVENT {eventName} {JsonConvert.SerializeObject(dict, Formatting.Indented)}");
        }
    }
}
    