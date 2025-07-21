using JetBrains.Annotations;
using Modules.CommonModule.Extensions;

namespace Modules.VFX
{
    [UsedImplicitly]
    public sealed class VfxManager
    {
        private readonly VfxSettings settings;

        public VfxManager(VfxSettings settings)
        {
            this.settings = settings;
        }

        public VfxHandle GetVfx(string key) 
            => settings.VfxHandles.Find(effect => effect.Key == key);
    }
}

