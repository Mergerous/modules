using UnityEngine;

namespace Modules.Common.Structures
{
    public class Tick
    {
        private float currentValue;
        private float maxValue;
        
        public Tick(float maxValue)
        {
            this.maxValue = maxValue;
        }
        
        public static implicit operator bool(Tick d)
        {
            if (d.currentValue >= d.maxValue)
            {
                d.currentValue = 0f;
                return true;
            }

            d.currentValue += Time.deltaTime;
            return false;
        }
    }
}
