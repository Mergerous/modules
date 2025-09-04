using UnityEngine;

namespace Modules.Debugging
{
    public static class Logger
    {
        public static void Log(object message, Object context)
        {
#if UNITY_EDITOR || !RELEASE_BUILD
            Debug.Log(message, context);
#endif
        }
        
        public static void LogWarning(object message, Object context)
        {
#if UNITY_EDITOR || !RELEASE_BUILD
            Debug.LogWarning(message, context);
#endif
        }
    }
}