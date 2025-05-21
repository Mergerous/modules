using JetBrains.Annotations;
using Modules.Debugging;
using UnityEngine.UIElements;

namespace Consumables.Currencies
{
    [UsedImplicitly]
    public sealed class CurrenciesDebug : IDebuggable
    {
        private readonly ICurrenciesProcessor currenciesesProcessor;

        public CurrenciesDebug(CurrenciesManager currenciesesManager)
        {
            currenciesesProcessor = currenciesesManager;
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

            // DropdownField dropdown = new DropdownField
            // {
            //     choices = new List<string>()
            //     {
            //         ConsumableNames.Dollar
            //     },
            //     style =
            //     {
            //         flexGrow = 1f
            //     },
            //     index = 0
            // };

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
            //
            // getButton.clicked += () => currencyProcessor.AddCurrency<CurrencyModel>(dropdown.value, countField.value);
            //
            // layout.Add(dropdown);
            layout.Add(countField);
            layout.Add(getButton);

            root.Add(label);
            root.Add(layout);
            
            return root;
        }
    }
}
