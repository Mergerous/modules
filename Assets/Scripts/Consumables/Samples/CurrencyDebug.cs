using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Debugging;
using UnityEngine.UIElements;

namespace Consumables
{
    [UsedImplicitly]
    public sealed class CurrencyDebug : IDebuggable
    {
        private readonly ICurrencyProcessor currencyProcessor;

        public CurrencyDebug(CurrencyManager currencyManager)
        {
            currencyProcessor = currencyManager;
        }

        public VisualElement CreateDebugElement()
        {
            VisualElement root = new Box();
            Label label = new Label("Currency");
            VisualElement layout = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };

            DropdownField dropdown = new DropdownField
            {
                choices = new List<string>()
                {
                    ConsumableNames.Dollar
                },
                style =
                {
                    flexGrow = 1f
                },
                index = 0
            };

            IntegerField countField = new IntegerField
            {
                style =
                {
                    flexGrow = 1f
                }
            };

            Button getButton = new Button
            {
                style =
                {
                    flexGrow = 1f
                },
                text = "Add Currency"
            };
            
            getButton.clicked += () => currencyProcessor.AddCurrency<CurrencyModel>(dropdown.value, countField.value);
            
            layout.Add(dropdown);
            layout.Add(countField);
            layout.Add(getButton);

            root.Add(label);
            root.Add(layout);
            
            return root;
        }
    }
}
