using System;
using R3;

namespace Modules.Times
{
    public interface ITimeContent
    {
        DateTime Now { get; }
        Observable<DateTime> NowObservable { get; }
    }
}
