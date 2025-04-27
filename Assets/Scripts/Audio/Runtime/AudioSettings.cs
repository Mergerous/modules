using System.Collections.Generic;
using UnityEngine;

namespace Modules.Audio.Settings
{
    [CreateAssetMenu(menuName = "Settings/AudioSettings", fileName = "AudiosSettings")]
    public class AudioSettings : ScriptableObject
    {
        [Header("Options")] 
        [field: SerializeField] public bool IsMusicEnabled = true;
        [field: SerializeField] public bool IsSoundsEnabled = true;
        [field: SerializeField] public bool ConvertToDictionary = true;

        [Header("Components")] 
        [field: SerializeField] public List<Mixer> Mixers;
        [field: SerializeField] public List<Sound> Sounds;
        [field: SerializeField] public List<Melody> Melodies;
    }
}
