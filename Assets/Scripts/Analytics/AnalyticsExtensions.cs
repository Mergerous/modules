using System.Collections.Generic;

namespace Modules.Analytics
{
    public static class AnalyticsExtensions
    {
        public static Dictionary<string, object> CreateDictionary(params (string, object)[] parameters)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach ((string parameterName, object parameterValue) in parameters)
            {
                dict.TryAdd(parameterName, parameterValue);
            }
            return dict;
        }
        
        public static void TrackEvent(this IAnalysable source, params (string, object)[] parameters)
        {
            
        }
    }
}
