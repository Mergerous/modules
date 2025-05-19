using System;

namespace Modules.Times
{
    public interface ITimeProcessor
    {
        void AddShift(TimeSpan shift);
        void RemoveShift(TimeSpan shift);
    }
}