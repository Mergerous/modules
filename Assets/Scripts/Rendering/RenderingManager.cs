namespace Modules.Rendering
{
    public class RenderingManager
    {
        private readonly RenderingSettings _renderingSettings;

        public RenderingManager(RenderingSettings renderingSettings)
        {
            _renderingSettings = renderingSettings;
        }

        public T GetRenderFeature<T>(string featureName) where T : RenderFeature
        {
            foreach (RenderFeature renderFeature in _renderingSettings.RenderFeatures)
            {
                if (renderFeature.Name == featureName && renderFeature is T targetRenderFeature)
                {
                    return targetRenderFeature;
                }
            }
            return default;
        }
    }
}
