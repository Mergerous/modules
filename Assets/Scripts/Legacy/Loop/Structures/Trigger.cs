using Modules.Loop.Managers;
using UnityEngine;

namespace Modules.Common.Structures
{
    public class Trigger
    {
        private int frameCount;

        public void Invoke(int frame = 0)
        {
            frameCount = Time.frameCount + frame;
        }
        
        public static implicit operator bool(Trigger d)
        {
            return d.frameCount >= Time.frameCount;
        }
    }

    public class Trigger<T>
    {
        private int frameCount;
        private T value;

        public void Invoke(T value)
        {
            this.value = value;
            frameCount = LoopManager.FramesCount;
        }

        public bool IsTriggered(out T value)
        {
            value = this.value;
            return frameCount >= LoopManager.FramesCount;
        }
    }

    public class TriggerA<T>
    {
        public Trigger<T> listeners;
        
    }
}
