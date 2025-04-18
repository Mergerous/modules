using System;
using System.Collections.Generic;
using Consumables;
using JetBrains.Annotations;

namespace Shop
{
    [UsedImplicitly]
    public sealed class ConsumablesRequirementProcess : IRequirementProcess
    {
        private readonly ICurrencyProcessor currencyProcessor;
        public void Buy(IShopContent model, IEnumerable<ISenderData> senderData, Action<IShopContent> acceptCallback)
        {
            if (model.Requirement is ConsumablesRequirement consumablesRequirement
                && currencyProcessor.TryRemoveCurrency(consumablesRequirement.currencyData.key, consumablesRequirement.currencyData.value))
            {
                acceptCallback?.Invoke(model);
            }
        }
    }
}