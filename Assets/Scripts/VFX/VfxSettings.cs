using System.Collections.Generic;
using UnityEngine;

namespace Modules.VFX
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(VfxSettings), fileName = nameof(VfxSettings))]
    public sealed class VfxSettings : ScriptableObject
    {
        [SerializeField] private VfxHandle[] vfxHandles;

        public IEnumerable<VfxHandle> VfxHandles => vfxHandles;
    }
}
