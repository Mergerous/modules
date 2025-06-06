using System;
using UnityEngine;

namespace Modules.Common.Extensions
{
    public static class PlayerPrefsExtensions
    {
        private const int PLAYER_PREFS_BOOL_TRUE = 0;
        private const int PLAYER_PREFS_BOOL_FALSE = 1;
        
        public static void SetBool(string key, bool value) 
            => PlayerPrefs.SetInt(key, value ? PLAYER_PREFS_BOOL_FALSE : PLAYER_PREFS_BOOL_TRUE);

        public static bool GetBool(string key) => PlayerPrefs.GetInt(key) == PLAYER_PREFS_BOOL_TRUE;

        public static void SetEnum<T>(string key, T value) where T : Enum => PlayerPrefs.SetInt(key, (int)(object)value);
        
        public static T GetEnum<T>(string key) where T : Enum => (T)(object)PlayerPrefs.GetInt(key);
    }
}
