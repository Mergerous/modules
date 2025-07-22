using System;
using Modules.Loop;
using UnityEngine;

namespace Modules.Rendering
{
    [Serializable]
    public class ShaderPropertyRenderFeature : RenderFeature, IUpdatable
    {
        private enum ShaderType
        {
            Global = 2
        }
        
        private enum ShaderPropertyType
        {
            None = 0,
            Integer = 1,
            Float = 2,
            Vector = 3,
            Texture = 4,
            Matrix = 5,
            Color = 6,
            Buffer = 7
        }
        
        [SerializeField] private string shaderPropertyName;
        [SerializeField] private ShaderType shaderType;
        [SerializeField] private ShaderPropertyType shaderPropertyType;
        [SerializeField] private Material[] materials;
        
        public Vector4 Vector { private get; set; }
        public int Integer { private get; set; }
        public Texture Texture { private get; set; }
        public Matrix4x4 Matrix { private get; set; }
        public Color Color { private get; set; }
        public float Float { private get; set; }
        public ComputeBuffer Buffer { private get; set; }
        private int PropertyID => Shader.PropertyToID(shaderPropertyName);
        
        public UpdateType UpdateType => UpdateType.Update;
        public float DeltaTime { get; set; }


        public void Update()
        {
            switch (shaderType, shaderPropertyType)
            {
                case (ShaderType.Global, ShaderPropertyType.Vector):
                    Shader.SetGlobalVector(PropertyID, Vector);
                    break;
                case (ShaderType.Global, ShaderPropertyType.Integer):
                    Shader.SetGlobalInteger(PropertyID, Integer);
                    break;
                case (ShaderType.Global, ShaderPropertyType.Texture):
                    Shader.SetGlobalTexture(PropertyID, Texture);
                    break;
                case (ShaderType.Global, ShaderPropertyType.Matrix):
                    Shader.SetGlobalMatrix(PropertyID, Matrix);
                    break;
                case (ShaderType.Global, ShaderPropertyType.Color):
                    Shader.SetGlobalColor(PropertyID, Color);
                    break;
                case (ShaderType.Global, ShaderPropertyType.Float):
                    Shader.SetGlobalFloat(PropertyID, Float);
                    break;
                case (ShaderType.Global, ShaderPropertyType.Buffer):
                    Shader.SetGlobalBuffer(PropertyID, Buffer);
                    break;
            }
        }

        public override void Initialize()
        {
            this.Start();
        }
    }
}