using System.Reflection;

namespace Modules.States
{
    public static class StatesExtensions
    {
        public static bool TryGetKey(this IState source, out string key)
        {
            var attribute = source.GetType().GetCustomAttribute<StateAttribute>();
            if (attribute != null)
            {
                key = attribute.Key;
                return true;
            }

            key = default;
            return false;
        }
    }
}
