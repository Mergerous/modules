using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Debugging
{
    [UsedImplicitly]
    public sealed class DebugManager
    {
        private readonly IEnumerable<IDebuggable> debuggables;
        private readonly DebugContainer debugContainer;
        private VisualElement root;
        private bool isEnabled;
        
        public DebugManager(DebugContainer debugContainer, IEnumerable<IDebuggable> debuggables)
        {
            this.debugContainer = debugContainer;
            this.debuggables = debuggables;
        }
        
        public void Execute()
        {
            root = debugContainer.Document.rootVisualElement;
            root.style.backgroundColor = Color.black;
            
            foreach (IDebuggable debuggable in debuggables)
            {
                root.Add(debuggable.CreateDebugElement());
            }
            
            root.visible = isEnabled;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                root.visible = isEnabled = !isEnabled;
            }
        }
    }
}
