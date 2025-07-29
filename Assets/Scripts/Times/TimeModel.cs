using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using R3;
using Times;

namespace Modules.Times
{
    [UsedImplicitly]
    public sealed class TimeModel : ITimeContent, ITimerContent
    {
        private readonly ReactiveProperty<double> shift;
        private readonly HashSet<ITimer> timers = new();
        
        public TimeData Data { get; }
        public DateTime Now => DateTime.Now + TimeSpan.FromSeconds(Shift);
        public Observable<DateTime> NowObservable => ShiftObservable
            .Select(shift => DateTime.Now + TimeSpan.FromSeconds(shift));

        public Observable<double> ShiftObservable => shift;
        public IEnumerable<ITimer> Timers => timers;

        public double Shift
        {
            get => shift.Value;
            set => shift.Value = value;
        }

        public TimeModel(TimeData data)
        {
            Data = data;
            shift = new ReactiveProperty<double>(Data.shift);
            shift.Subscribe(shift => Data.shift = shift);
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