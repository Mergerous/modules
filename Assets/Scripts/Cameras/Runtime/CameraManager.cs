using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;

namespace Modules.Cameras
{
    [UsedImplicitly]
    public sealed class CameraManager
    {
        private readonly CamerasContainer camerasContainer;
        
        public Camera MainCamera => Camera.main;

        public CameraManager(CamerasContainer camerasContainer)
        {
            this.camerasContainer = camerasContainer;
        }

        public void SetState(string key, int state)
        {
            camerasContainer.StateDrivenAnimator.SetInteger(Animator.StringToHash(key), state);
        }

        public void SetTrigger(string trigger)
        {
            camerasContainer.StateDrivenAnimator.SetTrigger(Animator.StringToHash(trigger));
        }

        public CinemachineVirtualCameraBase GetChildCamera(int index)
        { 
            return camerasContainer.CinemachineStateDrivenCamera.ChildCameras[index];
        }

        public void AddTargetGroupMember(Transform transform, float weight, float radius)
        {
            camerasContainer.CinemachineTargetGroup.AddMember(transform, weight, radius);
        }

        public void RemoveTargetGroupMember(Transform transform)
        {
            camerasContainer.CinemachineTargetGroup.RemoveMember(transform);
        }
    }

}