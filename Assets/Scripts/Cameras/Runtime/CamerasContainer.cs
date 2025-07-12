using Unity.Cinemachine;
using UnityEngine;

namespace Modules.Cameras
{
    public class CamerasContainer : MonoBehaviour
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public Animator StateDrivenAnimator { get; private set; }
        [field: SerializeField] public CinemachineTargetGroup TargetGroup { get; private set; }
        [field: SerializeField] public CinemachineStateDrivenCamera StateDrivenCamera { get; private set; }
    }
}