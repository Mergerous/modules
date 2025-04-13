using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Modules.Remote
{
    [CustomPropertyDrawer(typeof(SheetFeature))]
    public class SheetFeatureEditor : PropertyDrawer
    {
        private const string REQUEST_PATTERN = "https://script.google.com/macros/s/{0}/exec?spreadsheet={1}&gid={2}";
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty nameProperty = property.FindPropertyRelative(nameof(SheetFeature.name));
            SerializedProperty macrosProperty = property.FindPropertyRelative(nameof(SheetFeature._macrosId));
            SerializedProperty spreadsheetProperty = property.FindPropertyRelative(nameof(SheetFeature._spreadsheetId));
            SerializedProperty gidProperty = property.FindPropertyRelative(nameof(SheetFeature._gid));
            SerializedProperty csvProperty = property.FindPropertyRelative(nameof(SheetFeature.csvFile));
            SerializedProperty loadProperty = property.FindPropertyRelative(nameof(SheetFeature.loadType));
            
            VisualElement root = new VisualElement();
            VisualElement csvGroup = new VisualElement()
            {
                style = { flexDirection = FlexDirection.Row, }
            };
            VisualElement spreadsheetGroup = new VisualElement()
            {
                style = { flexDirection = FlexDirection.Row, }
            };
            PropertyField nameField = new PropertyField(nameProperty);
            PropertyField macrosField = new PropertyField(macrosProperty);
            PropertyField spreadSheetField = new PropertyField(spreadsheetProperty);
            PropertyField gidField = new PropertyField(gidProperty);
            PropertyField csvField = new PropertyField(csvProperty);
            EnumField loadField = new EnumField(LoadType.None)
            {
                value = (LoadType)loadProperty.enumValueIndex
            };
            loadField.RegisterValueChangedCallback(x =>
            {
                loadProperty.enumValueIndex = (int)(LoadType)x.newValue;

                if ((LoadType)x.newValue == LoadType.Csv)
                {
                    root.Add(csvGroup);
                }
                else if(root.Contains(csvGroup))
                {
                    root.Remove(csvGroup);
                }
            });
            
            Button parseButton = new Button(async () =>
            {
                var url = string.Format(REQUEST_PATTERN, macrosProperty.stringValue, spreadsheetProperty.stringValue, gidProperty.stringValue);
                var csvFile = property.FindPropertyRelative(nameof(SheetFeature.csvFile)).objectReferenceValue;
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                string text = await response.Content.ReadAsStringAsync();
                await File.WriteAllTextAsync(AssetDatabase.GetAssetPath(csvFile), text);
            })
            {
                text = "Parse",
                style =
                {
                    flexGrow = 1
                }
            };
            
            csvGroup.Add(csvField);
            csvGroup.Add(parseButton);
            spreadsheetGroup.Add(spreadSheetField);
            spreadsheetGroup.Add(gidField);
                
            root.Add(nameField);
            root.Add(loadField);
            root.Add(macrosField);
            root.Add(spreadsheetGroup);
            if ((LoadType)loadProperty.enumValueIndex == LoadType.Csv)
            {
                root.Add(csvGroup);
            }

            return root;
        }
    }
}
