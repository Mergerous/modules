using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;

namespace Modules.Cameras
{
    [UsedImplicitly]
    public sealed class CameraManager
    {
        private readonly CamerasContainer camerasContainer;
        
        public Camera MainCamera => camerasContainer.MainCamera;

        public CameraManager(CamerasContainer camerasContainer)
        {
            this.camerasContainer = camerasContainer;
        }

        public void SetState(int id, int value)
        {
            camerasContainer.StateDrivenAnimator.SetInteger(id, value);
        }

        public void SetTrigger(string trigger)
        {
            camerasContainer.StateDrivenAnimator.SetTrigger(Animator.StringToHash(trigger));
        }

        public CinemachineVirtualCameraBase GetChildCamera(int index)
        { 
            return camerasContainer.StateDrivenCamera.ChildCameras[index];
        }

        public void AddTargetGroupMember(Transform transform, float weight, float radius)
        {
            camerasContainer.TargetGroup.AddMember(transform, weight, radius);
        }

        public void RemoveTargetGroupMember(Transform transform)
        {
            camerasContainer.TargetGroup.RemoveMember(transform);
        }
    }

}