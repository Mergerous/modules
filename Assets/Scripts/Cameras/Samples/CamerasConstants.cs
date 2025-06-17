using UnityEngine;

namespace Modules.Cameras
{
    public static class CamerasConstants
    {
        public const string STATE_KEY = "State";
        public static readonly int StateHash = Animator.StringToHash(STATE_KEY);
    }
}