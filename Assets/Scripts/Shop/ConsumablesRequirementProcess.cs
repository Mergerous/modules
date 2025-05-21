using System;
using System.Collections.Generic;
using Consumables.Currencies;
using JetBrains.Annotations;

namespace Shop
{
    [UsedImplicitly]
    public sealed class ConsumablesRequirementProcess : IRequirementProcess
    {
        private readonly ICurrenciesProcessor currenciesProcessor;

        public ConsumablesRequirementProcess(ICurrenciesProcessor currenciesProcessor)
        {
            this.currenciesProcessor = currenciesProcessor;
        }

        public void Buy(IShopContent model, IEnumerable<ISenderData> senderData, Action<IShopContent> acceptCallback)
        {
            if (model.Requirement is ConsumablesRequirement consumablesRequirement
                && currenciesProcessor.TryRemoveCurrency(consumablesRequirement.currencyData.key, consumablesRequirement.currencyData.value))
            {
                acceptCallback?.Invoke(model);
            }
        }
    }
}