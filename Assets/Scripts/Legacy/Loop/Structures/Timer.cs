using System;
using Modules.Loop;
using Modules.Loop.Interfaces;

namespace Modules.Common.Structures
{
    public class Timer : IUpdatable
    {
        private event Action<TimeSpan> tickCallback;
        private event Action endCallback;
        private TimeSpan _currentSpan;
        private readonly TimeSpan _targetSpan;
        private readonly bool _isBackward;

        public Timer(TimeSpan span, bool isBackward = true, Action<TimeSpan> tickCallback = null, Action endCallback = null)
        {
            _isBackward = isBackward;
            this.tickCallback = tickCallback;
            this.endCallback = endCallback;
            (_currentSpan, _targetSpan) = _isBackward ? (span, TimeSpan.Zero) : (TimeSpan.Zero, span);
        }
        
        public void Update(float deltaTime)
        {
            TimeSpan tick = TimeSpan.FromSeconds(deltaTime) * (_isBackward ? -1 : 1);
            if (_currentSpan > _targetSpan)
            {
                _currentSpan += tick;
                tickCallback?.Invoke(_currentSpan);
            }
            else
            {
                endCallback?.Invoke();
                this.Stop();
            }
        }
    }
}
