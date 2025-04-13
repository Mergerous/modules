using Unity.Cinemachine;
using UnityEngine;

namespace Modules.Cameras
{
    public class CamerasContainer : MonoBehaviour
    {
        [SerializeField] private Animator _stateDrivenAnimator;
        [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup;
        [SerializeField] private CinemachineStateDrivenCamera _cinemachineStateDriven;

        public CinemachineTargetGroup CinemachineTargetGroup => _cinemachineTargetGroup;
        public CinemachineStateDrivenCamera CinemachineStateDrivenCamera => _cinemachineStateDriven;
        public Animator StateDrivenAnimator => _stateDrivenAnimator;
    }
}