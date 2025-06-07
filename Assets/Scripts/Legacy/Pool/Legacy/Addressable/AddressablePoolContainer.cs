#if ADDRESSABLES
using Pool;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.Pool{
[CreateAssetMenu]
public class AddressablePoolContainer : PoolContainer<AssetReference>
{

}
}
#endif