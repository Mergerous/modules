using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.CommonModule.Extensions;

namespace Modules.Common
{
    [UsedImplicitly]
    public sealed class KeysManager
    {
        private readonly KeysLibrary keysLibrary;

        public KeysManager(KeysLibrary keysLibrary)
        {
            this.keysLibrary = keysLibrary;
        }

        public IEnumerable<string> GetKeyVariants(int id)
        {
            KeysSettings keysSettings = keysLibrary.Settings.Find(settings => settings.id == id);
            return keysSettings.variants;
        }

        public int GetValueIndex(int id, string value)
        {
            KeysSettings keysSettings = keysLibrary.Settings.Find(settings => settings.id == id);
            return keysSettings.variants.IndexOf(value);
        }
    }
}
