using UnityEngine;

namespace Modules.Rendering
{
    [CreateAssetMenu(menuName = "Settings/Rendering", fileName = "RenderingSettings", order = 0)]
    public class RenderingSettings : ScriptableObject
    {
        [SerializeReference] private RenderFeature[] _renderFeatures;

        public RenderFeature[] RenderFeatures => _renderFeatures;
    }
}