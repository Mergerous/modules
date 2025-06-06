using System;
using System.Linq;
using System.Reflection;

namespace Modules.Common.Extensions
{
    public static class EnumExtensions
    {
        public static T GetElementWithHighestPriority<T>(this T flags) where T : Enum 
        {
            T values = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(x => flags.HasFlag(x))
                .OrderByDescending(GetPriority)
                .FirstOrDefault();

            return values;
        }

        private static int GetPriority<T>(T value) {
            FieldInfo field = value.GetType().GetField(value.ToString());
            PriorityAttribute attribute = field.GetCustomAttribute<PriorityAttribute>(false);
            return attribute?.Priority ?? 0;
        }

        public static T[] All<T>(this T source) where T : Enum
        {
            return Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .ToArray();
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class PriorityAttribute : Attribute 
    {
        public int Priority { get; }

        public PriorityAttribute(int priority) 
        {
            Priority = priority;
        }
    }
    
    public class HasFlag : IDisposable
    {
        private Enum source;

        public HasFlag(Enum source)
        {
            this.source = source;
        }

        public Action this[Enum c, bool has = true]
        {
            set
            {
                if (has == source.HasFlag(c))
                {
                    value();
                }
            }
        }

        public void Dispose()
        {
            source = default;
        }
    }
}