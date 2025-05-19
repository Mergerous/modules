using System;
using JetBrains.Annotations;
using Modules.Data;
using Times;
using VContainer.Unity;

namespace Modules.Times
{
    [UsedImplicitly]
    public sealed class TimeManager : ITickable, ITimeProcessor
    {
        private readonly DataManager dataManager;
        private readonly TimeModel timeModel;

        public TimeManager(DataManager dataManager, TimeModel timeModel)
        {
            this.dataManager = dataManager;
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
            timeModel.Data.shift += shift.TotalSeconds;
            dataManager.Save(TimeConstants.TIME_DATA_SAVE_KEY, timeModel.Data);
        }
        
        public void RemoveShift(TimeSpan shift)
        {
            timeModel.Data.shift -= shift.TotalSeconds;
            dataManager.Save(TimeConstants.TIME_DATA_SAVE_KEY, timeModel.Data);
        }

        public void AddTimer(ITimer timer)
        {
            
        }

        public void RemoveTimer(ITimer timer)
        {
            
        }
    }
}