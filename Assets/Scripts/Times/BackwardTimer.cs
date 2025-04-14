using R3;
using UnityEngine;

namespace Times
{
    public sealed class BackwardTimer : ITimer
    {
        private readonly ReactiveProperty<int> seconds = new();
        private float tick;
        
        public Observable<int> SecondsObservable => seconds;

        public int Seconds
        {
            get => seconds.Value;
            set => seconds.Value = value;
        }

        public void Update()
        {
            if (Seconds <= 0f)
            {
                return;
            }
            
            tick += Time.deltaTime;
            if (tick >= 1f)
            {
                Seconds -= 1;
                tick = 0f;
            }
        }
    }
}
