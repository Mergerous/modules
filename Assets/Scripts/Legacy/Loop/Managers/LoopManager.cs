using System.Collections.Generic;
using Modules.Common.Extensions;
using Modules.Loop.Interfaces;
using UnityEngine;

namespace Modules.Loop.Managers
{
    public class LoopManager : MonoBehaviour
    {
        public static int FramesCount;
        
        private readonly List<IUpdatable> updatables = new List<IUpdatable>();
        private readonly List<IUpdatable> fixedUpdatables = new List<IUpdatable>();
        private readonly List<IUpdatable> lateUpdatables = new List<IUpdatable>();
        private readonly List<IExecutable> executables = new List<IExecutable>();

        private void Awake()
        {
            LoopExtensions.LoopManager = this;
        }

        private void Start()
        {
            for (int i = 0; i < executables.Count; i++)
            {
                executables[i].Execute();
            }
        }

        
        private void Update()
        {
            for (int i = 0; i < updatables.Count; i++)
            {
                updatables[i].Update(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < fixedUpdatables.Count; i++)
            {
                fixedUpdatables[i].Update(Time.fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < lateUpdatables.Count; i++)
            {
                lateUpdatables[i].Update(Time.deltaTime);
            }
            
            FramesCount++;
        }

        private void OnDestroy()
        {
            FramesCount = 0;
        }

        public void Add(IExecutable executable)
        {
            executables.Add(executable);
        }
        
        public void Add(IUpdatable updatable)
        {
            HasFlag hasFlag = new HasFlag(updatable.UpdateType)
            {
                [UpdateType.Update] = () => updatables.Add(updatable),
                [UpdateType.FixedUpdate] = () => fixedUpdatables.Add(updatable),
                [UpdateType.LateUpdate] = () => lateUpdatables.Add(updatable),
            };
        }

        
        public void Remove(IUpdatable updatable)
        {
            HasFlag hasFlag = new HasFlag(updatable.UpdateType)
            {
                [UpdateType.Update] = () => updatables.Remove(updatable),
                [UpdateType.FixedUpdate] = () => fixedUpdatables.Remove(updatable),
                [UpdateType.LateUpdate] = () => lateUpdatables.Remove(updatable),
            };
        }
    }
}
