using System;
using Data.Runtime;
using JetBrains.Annotations;
using Modules.Data;
using Times;
using VContainer.Unity;

namespace Modules.Times
{
    [UsedImplicitly]
    public sealed class TimeManager : ITickable, ITimeProcessor
    {
        private readonly IDataService dataService;
        private readonly TimeModel timeModel;

        public TimeManager(IDataService dataService, TimeModel timeModel)
        {
            this.dataService = dataService;
            this.timeModel = timeModel;
        }

        public void Tick()
        {
            foreach (ITimer timer in timeModel.Timers)
            {
                timer.Update();
            }
        }

        public void AddShift(TimeSpan shift)
        {
            timeModel.Shift += shift.TotalSeconds;
            dataService.Save(TimeConstants.TIME_DATA_SAVE_KEY, timeModel.Data);
        }
        
        public void RemoveShift(TimeSpan shift)
        {
            timeModel.Shift -= shift.TotalSeconds;
            dataService.Save(TimeConstants.TIME_DATA_SAVE_KEY, timeModel.Data);
        }

        public void AddTimer(ITimer timer)
        {
            
        }

        public void RemoveTimer(ITimer timer)
        {
            
        }
    }
}