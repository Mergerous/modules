using System.Collections.Generic;
using Consumables.Currencies;
using JetBrains.Annotations;
using Modules.Debugging;
using UnityEngine.UIElements;

namespace Consumables
{
    [UsedImplicitly]
    public sealed class CurrenciesDebug : IDebuggable
    {
        private readonly ICurrenciesProcessor<CurrencyData> currenciesProcessor;

        public CurrenciesDebug(ICurrenciesProcessor<CurrencyData> currenciesProcessor)
        {
            this.currenciesProcessor = currenciesProcessor;
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
                    ConsumablesNames.DOLLAR
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
            
            getButton.clicked += () => currenciesProcessor.AddCurrency(new CurrencyData(dropdown.value, countField.value));
            
            layout.Add(dropdown);
            layout.Add(countField);
            layout.Add(getButton);

            root.Add(label);
            root.Add(layout);
            
            return root;
        }
    }
}