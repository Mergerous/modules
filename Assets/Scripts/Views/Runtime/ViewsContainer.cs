using UnityEngine;

namespace Modules.Views
{
    public class ViewsContainer : MonoBehaviour
    {
        [field: SerializeField] public Transform Parent { get; private set; }
    }
}