using System.Collections.Generic;
using Firebase.Analytics;
using Newtonsoft.Json;
using UnityEngine;

namespace Modules.Analytics
{
    public class AnalyticsManager
    {
        public AnalyticsManager()
        {
        }

        public void Register(IAnalysable source)
        {
            source.OnEventTracked += TrackEvent;
        }


        public void Unregister(IAnalysable source)
        {
            source.OnEventTracked -= TrackEvent;
        }
        
        public void TrackEvent(string key, AnalyticsArguments arguments)
        {
            switch (arguments.type)
            {
                case TrackEventType.Object:
                    TrackEvent(key, arguments.obj);
                    break;
                case TrackEventType.Json:
                    TrackEvent(key, arguments.json);
                    break;
                case TrackEventType.Parameters:
                    TrackEvent(key, arguments.parameters);
                    break;
                default:
                    TrackEvent(key);
                    break;
            }
        }

        public void TrackEvent(string name)
        {
            FirebaseAnalytics.LogEvent(name);
            // _appMetrica.ReportEvent(name);
            Debug.Log($"LOG_EVENT {name}");
        }

        public void TrackEvent(string name, object obj)
        {
            TrackEvent(name, JsonConvert.SerializeObject(obj));
        }

        public void TrackEvent(string name, string json)
        {
            // FirebaseAnalytics.LogEvent(name, );
            // _appMetrica.ReportEvent(name, json);
            Debug.Log($"LOG_EVENT {name} {json}");
        }

        public void TrackEvent(string name, Dictionary<string, object> dict)
        {
            // _appMetrica.ReportEvent(name, dict);
            Debug.Log($"LOG_EVENT {name} {JsonConvert.SerializeObject(dict, Formatting.Indented)}");
        }
    }
}
    