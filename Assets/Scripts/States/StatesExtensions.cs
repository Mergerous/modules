using System.Reflection;

namespace Modules.States
{
    public static class StatesExtensions
    {
        public static string GetKey(this IState source)
        {
            var attribute = source.GetType().GetCustomAttribute<StateAttribute>();
            return attribute?.Key;
        }
    }
}
