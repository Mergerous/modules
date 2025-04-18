using System.Collections.Generic;
using JetBrains.Annotations;

namespace Shop
{
    [UsedImplicitly]
    public sealed class ShopManager : IShopProcessor
    {
        private readonly IEnumerable<IGainProcess> gainProcesses;
        private readonly IEnumerable<IRequirementProcess> requirementProcesses;

        public ShopManager(IEnumerable<IGainProcess> gainProcesses, IEnumerable<IRequirementProcess> requirementProcesses)
        {
            this.gainProcesses = gainProcesses;
            this.requirementProcesses = requirementProcesses;
        }

        public void Buy(IShopContent shopContent, params ISenderData[] senderData)
        {
            if (HasGain(shopContent))
            {
                Apply(shopContent);
                return;
            }
            
            foreach (IRequirementProcess requirementProcess in requirementProcesses)
            {
                requirementProcess.Buy(shopContent, senderData, Apply);
            }
        }
        
        public bool HasGain(IShopContent shopContent)
        {
            foreach (var gainProcess in gainProcesses)
            {
                if (gainProcess.HasGain(shopContent))
                {
                    return true;
                }
            }
            
            return false;
        }

        public void Apply(IShopContent shopContent)
        {
            foreach (var gainProcess in gainProcesses)
            {
                gainProcess.Apply(shopContent);
            }
        }
    }
}