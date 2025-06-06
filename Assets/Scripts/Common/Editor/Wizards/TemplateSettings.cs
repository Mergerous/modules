using UnityEngine;

namespace Modules.Common.Editor.Wizards
{
    [CreateAssetMenu(menuName = "Settings/Templates/" + nameof(TemplateSettings), fileName = nameof(TemplateSettings))]
    public class TemplateSettings : ScriptableObject
    {
        [field: SerializeField] public Folder[] Folders { get; private set; } = new Folder[]
        {
            new(CustomFolderCreator.REPLACE_PATTERN)
            {
                folders = new Folder[]
                {
                    new("Managers"),
                    new("Controllers"),
                    new("Models"),
                    new("Configs"),
                    new("Views"),
                    new("Enums"),
                    new("Constants"),
                    new("Systems"),
                    new("Windows")
                }
            }
        };
    }
}
