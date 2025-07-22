using System;
using Modules.Common;
using Modules.Common.Editor;
using UnityEditor;

namespace Modules.Rendering
{
    [CustomEditor(typeof(RenderingSettings))]
    public class RenderingSettingsEditor : SettingsEditor
    {
        protected override string ManagedReference => "_renderFeatures";
        protected override Type TargetType => typeof(RenderFeature);
    }
}