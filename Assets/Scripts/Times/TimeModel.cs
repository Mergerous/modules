using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Times;

namespace Modules.Times
{
    [UsedImplicitly]
    public sealed class TimeModel : ITimeContent, ITimerContent
    {
        private readonly HashSet<ITimer> timers = new();
        
        public TimeData Data { get; }
        public DateTime Now => DateTime.Now + TimeSpan.FromSeconds(Data.shift);
        public IEnumerable<ITimer> Timers => timers;

        public TimeModel(Func<TimeData> dataFactory)
        {
            Data = dataFactory();
        }

        public T AddTimer<T>() where T : ITimer, new()
        {
            T timer = new T();
            timers.Add(timer);
            return timer;
        }

        public void RemoveTimer(ITimer timer)
        {
            timers.Remove(timer);
        }
    }
}