using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;

namespace Modules.Cameras
{
    [UsedImplicitly]
    public sealed class CameraManager
    {
        private readonly CamerasContainer _camerasContainer;
        
        public Camera MainCamera => Camera.main;

        public CameraManager(CamerasContainer camerasContainer)
        {
            _camerasContainer = camerasContainer;
        }

        public CameraManager SetTrigger(string trigger)
        {
            _camerasContainer.StateDrivenAnimator.SetTrigger(Animator.StringToHash(trigger));
            return this;
        }

        public CinemachineVirtualCameraBase GetChildCamera(int index)
        { 
            return _camerasContainer.CinemachineStateDrivenCamera.ChildCameras[index];
        }

        public void AddTargetGroupMember(Transform transform, float weight, float radius)
        {
            _camerasContainer.CinemachineTargetGroup.AddMember(transform, weight, radius);
        }

        public void RemoveTargetGroupMember(Transform transform)
        {
            _camerasContainer.CinemachineTargetGroup.RemoveMember(transform);
        }
    }

}