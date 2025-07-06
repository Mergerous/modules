using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public class SoundStateComponent : StateComponent
    {
        [SerializeField] private bool canPlay;
        [SerializeField] private string soundElementKey = "sound";
        
        public override void Apply()
        {
            View.GetElement<SoundElement>(soundElementKey).CanPlay = canPlay;
        }
    }
}