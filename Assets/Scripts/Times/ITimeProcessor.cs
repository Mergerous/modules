using System;

namespace Modules.Time
{
    public interface ITimeProcessor
    {
        void AddShift(TimeSpan shift);
        void RemoveShift(TimeSpan shift);
    }
}