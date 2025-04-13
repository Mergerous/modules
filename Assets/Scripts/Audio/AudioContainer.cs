using UnityEngine;

namespace Modules.Audio
{
    public class AudioContainer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _melodySource;

        public AudioSource AudioSource => _audioSource;
        public AudioSource MelodySource => _melodySource;
    }
}