using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.VFX
{
    [Serializable]
    public sealed class VfxHandle
    {
        [field: SerializeField] public string Key { get; private set; }
        [field: SerializeField] private ParticleSystem ParticlePrefab { get; set; }
        [SerializeField] private float destroyDelay = 2f;

        private ParticleSystem instance;

        public ParticleSystem CreateParticle(Vector3 position)
        {
            if (instance != null)
            {
                Object.Destroy(instance.gameObject);
            }
            
            instance = Object.Instantiate(ParticlePrefab);
            instance.transform.position = position;
            DOVirtual.DelayedCall(destroyDelay, () =>
            {
                Object.Destroy(instance.gameObject);
            }).SetLink(instance.gameObject);
            return instance;
        }
        
    }
}
