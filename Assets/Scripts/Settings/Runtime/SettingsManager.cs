using System.Collections.Generic;
using JetBrains.Annotations;
using Settings;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class SettingsManager
    {
        private readonly IEnumerable<IToggleSettingsProcessor> toggleProcessors;
        private readonly IEnumerable<IButtonSettingsProcessor> buttonProcessors;

        public SettingsManager(IEnumerable<IToggleSettingsProcessor> toggleProcessors, 
            IEnumerable<IButtonSettingsProcessor> buttonProcessors)
        {
            this.toggleProcessors = toggleProcessors;
            this.buttonProcessors = buttonProcessors;
        }

        public void Process(ISettingsItemModel model)
        {
            foreach (IButtonSettingsProcessor toggleProcessor in buttonProcessors)
            {
                toggleProcessor.Process(model);
            }
        }
        
        public void Process(bool isOn, ISettingsItemModel model)
        {
            foreach (IToggleSettingsProcessor toggleProcessor in toggleProcessors)
            {
                toggleProcessor.Process(isOn, model);
            }
        }
    }
}