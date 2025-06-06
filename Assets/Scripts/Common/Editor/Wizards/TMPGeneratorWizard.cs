using System;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Modules.Common.Editor.Wizards
{
    // TODO CHECK IF OUTLINE PIXELS CORRECT
    public class TMPGeneratorWizard : ScriptableWizard
    {
        private const float EQUANITY_TOLERANCE = 0.05f;
        private const int ROUND_DECIMALS = 2;
        
        private const string OUTLINE_ON_KEYWORD = "OUTLINE_ON";
        private const string UNDERLAY_ON_KEYWORD = "UNDERLAY_ON";

        private const string OUTLINE_WIDTH_NAME = "_OutlineWidth";
        private const string OUTLINE_COLOR_NAME = "_OutlineColor";
        private const string FACE_DILATE_NAME = "_FaceDilate";
        
        private const string UNDERLAY_DILATE_NAME = "_UnderlayDilate";
        private const string UNDERLAY_COLOR_NAME = "_UnderlayColor";
        private const string UNDERLAY_OFFSET_X_NAME = "_UnderlayOffsetX";
        private const string UNDERLAY_OFFSET_Y_NAME = "_UnderlayOffsetY";
        private const string UNDERLAY_SOFTNESS_NAME = "_UnderlaySoftness";

        private const string MATERIALS_FOLDER_PATH = "Assets/TextMesh Pro/Materials";
        private const string MATERIALS_FOLDER_NAME = "Generated";


        [Header("Font")] 
        [SerializeField] private float fontSize;
        [SerializeField] private Color fontColor = Color.white;
        [Header("Outline")] 
        [SerializeField] private bool shouldUseOutline;
        [SerializeField] private float outlineWidth;
        [SerializeField] private Color outlineColor = Color.white;
        [Header("Underlay")] 
        [SerializeField] private bool shouldUseUnderlay;
        [SerializeField] private float underlayWidth;
        [SerializeField] private float underlayBlur;
        [SerializeField] private Color underlayColor = Color.white;
        [SerializeField] private Vector2 underlayOffset;

        private static TextMeshProUGUI tmpComponent;

        
        [MenuItem("CONTEXT/TextMeshProUGUI/Generate Material")]
        public static void Open(MenuCommand command)
        {
            tmpComponent = command.context as TextMeshProUGUI;
            DisplayWizard<TMPGeneratorWizard>(nameof(TMPGeneratorWizard), "Close", "Create");
        }

        
        private void OnWizardOtherButton()
        {
            TMP_FontAsset font = tmpComponent.font;
            tmpComponent.fontSize = fontSize;
            tmpComponent.color = fontColor;
          
            float ratio = (float)font.creationSettings.padding / font.creationSettings.pointSize;

            float outlineValue = (float)Math.Round(outlineWidth * 0.5f / (tmpComponent.fontSize * ratio), ROUND_DECIMALS);
            float underlayValue = (float)Math.Round(underlayWidth * 0.5f / (tmpComponent.fontSize * ratio), ROUND_DECIMALS);
            float blurValue = (float)Math.Round(underlayBlur * 0.5f / (tmpComponent.fontSize * ratio), ROUND_DECIMALS);
            Vector2 signedOffset = new Vector2(
                this.underlayOffset.x + (Mathf.Approximately(this.underlayOffset.x, 0f) ? 0f : Mathf.Sign(this.underlayOffset.x) * outlineWidth),
                this.underlayOffset.y + (Mathf.Approximately(this.underlayOffset.y, 0f) ? 0f : Mathf.Sign(this.underlayOffset.y) * outlineWidth));

            Vector2 underlayOffset = signedOffset / (tmpComponent.fontSize * ratio);
            
            underlayOffset = new Vector2((float)Math.Round(underlayOffset.x, ROUND_DECIMALS),
                (float)Math.Round(underlayOffset.y, ROUND_DECIMALS));
            
            foreach (string guid in AssetDatabase.FindAssets($"t:{nameof(Material)}"))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material fontMaterial = AssetDatabase.LoadAssetAtPath<Material>(path);
                
                if (fontMaterial.name.Contains(font.name) 
                    && CheckOutline(fontMaterial, outlineValue)
                    && CheckUnderlay(fontMaterial, underlayValue, underlayOffset, blurValue))
                {
                    SetMaterial(fontMaterial);
                    Debug.Log($"Font already exists : {fontMaterial.name}");
                    return;
                }
            }
            
            Material generatedAsset = new Material(tmpComponent.font.material);
            if (shouldUseOutline)
            {
                generatedAsset.EnableKeyword(OUTLINE_ON_KEYWORD);
                generatedAsset.SetFloat(OUTLINE_WIDTH_NAME, outlineValue);
                generatedAsset.SetColor(OUTLINE_COLOR_NAME, outlineColor);
                generatedAsset.SetFloat(FACE_DILATE_NAME, outlineValue);
            }
            else
            {
                generatedAsset.DisableKeyword(OUTLINE_ON_KEYWORD);
            }

            if (shouldUseUnderlay)
            {
                generatedAsset.EnableKeyword(UNDERLAY_ON_KEYWORD);
                generatedAsset.SetFloat(UNDERLAY_DILATE_NAME, underlayValue);
                generatedAsset.SetColor(UNDERLAY_COLOR_NAME, underlayColor);
                generatedAsset.SetFloat(UNDERLAY_OFFSET_X_NAME, underlayOffset.x);
                generatedAsset.SetFloat(UNDERLAY_OFFSET_Y_NAME, underlayOffset.y);
                generatedAsset.SetFloat(UNDERLAY_SOFTNESS_NAME, blurValue);
            }
            else
            {
                generatedAsset.DisableKeyword(UNDERLAY_ON_KEYWORD);
            }
            
         
            if (!AssetDatabase.IsValidFolder($"{MATERIALS_FOLDER_PATH}/{MATERIALS_FOLDER_NAME}"))
            {
                AssetDatabase.CreateFolder($"{MATERIALS_FOLDER_PATH}", MATERIALS_FOLDER_NAME);
            }
            
            StringBuilder builder = new StringBuilder();
            builder.Append($"{MATERIALS_FOLDER_PATH}/{MATERIALS_FOLDER_NAME}/{font.name}");
            builder.Append($"-font-{fontSize}px");
            if (shouldUseOutline)
            {
                builder.Append($"-outline-color-{ColorUtility.ToHtmlStringRGBA(outlineColor)}");
                builder.Append($"-width-{outlineWidth}px");
            }

            if (shouldUseUnderlay)
            {
                builder.Append($"-underlay-color-{ColorUtility.ToHtmlStringRGBA(underlayColor)}");
                builder.Append($"-offset-{this.underlayOffset.x}px-{this.underlayOffset.y}px");
                builder.Append($"-width-{underlayWidth}px");
                builder.Append($"-blur-{underlayBlur}px");
            }
            builder.Append(".mat");
            
            Debug.Log($"Created font : {builder}");
            AssetDatabase.CreateAsset(generatedAsset, builder.ToString());
            AssetDatabase.SaveAssets();
            SetMaterial(generatedAsset);
        }

        
        private void OnWizardCreate()
        {
            
        }


        private void SetMaterial(Material material)
        {
            tmpComponent.fontSharedMaterial = material;
            tmpComponent.ForceMeshUpdate();
            tmpComponent.UpdateVertexData();
            tmpComponent.UpdateFontAsset();
            tmpComponent.GraphicUpdateComplete();
        }
        
        
        private bool CheckOutline(Material material, float outlineValue)
        {
            if (material.IsKeywordEnabled(OUTLINE_ON_KEYWORD) && shouldUseOutline)
            {
                return Math.Abs(material.GetFloat(OUTLINE_WIDTH_NAME) - outlineValue) < EQUANITY_TOLERANCE
                       && Math.Abs(material.GetFloat(FACE_DILATE_NAME) - outlineValue) < EQUANITY_TOLERANCE
                       && material.GetColor(OUTLINE_COLOR_NAME) == outlineColor;
            } 
            return !material.IsKeywordEnabled(OUTLINE_ON_KEYWORD) && !shouldUseOutline;
        }


        private bool CheckUnderlay(Material material, float underlayValue, Vector2 offset, float blur)
        {
            if (material.IsKeywordEnabled(UNDERLAY_ON_KEYWORD) && shouldUseUnderlay)
            {
                return Math.Abs(material.GetFloat(UNDERLAY_DILATE_NAME) - underlayValue) < EQUANITY_TOLERANCE
                       && material.GetColor(UNDERLAY_COLOR_NAME) == underlayColor
                       && Math.Abs(material.GetFloat(UNDERLAY_OFFSET_X_NAME) - offset.x) < EQUANITY_TOLERANCE
                       && Math.Abs(material.GetFloat(UNDERLAY_OFFSET_Y_NAME) - offset.y) < EQUANITY_TOLERANCE
                       && Math.Abs(material.GetFloat(UNDERLAY_SOFTNESS_NAME) - blur) < EQUANITY_TOLERANCE;
            } 
            return !material.IsKeywordEnabled(UNDERLAY_ON_KEYWORD) && !shouldUseUnderlay;
        }
    }
}
