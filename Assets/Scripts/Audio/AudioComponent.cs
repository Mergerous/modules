using UnityEngine;

namespace Modules.Audio.Components
{
    public class AudioComponent : MonoBehaviour
    {
        [field: SerializeField] public AudioSource Source { get; private set; }
    }
}
