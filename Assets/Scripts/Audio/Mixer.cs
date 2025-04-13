using System;
using UnityEngine;
using UnityEngine.Audio;


namespace Modules.Audio
{
    [Serializable]

    public class Mixer
    {
        [field: SerializeField] public string Key { get; private set; }
        [field: SerializeField] public AudioMixer AudioMixer { get; private set; }
        
    }
}