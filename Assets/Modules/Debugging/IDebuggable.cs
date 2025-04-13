using UnityEngine.UIElements;

namespace Modules.Debugging
{
    public interface IDebuggable
    {
        VisualElement CreateDebugElement();
    }
}
