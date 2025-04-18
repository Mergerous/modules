using System;
using System.Collections.Generic;

namespace Shop
{
    public interface IRequirementProcess
    {
        void Buy(IShopContent model, IEnumerable<ISenderData> senderData, Action<IShopContent> acceptCallback);
    }
}