using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Debugging
{
    public sealed class DebugContainer : MonoBehaviour
    {
        [field: SerializeField] public UIDocument Document { get; private set; }
    }
}
