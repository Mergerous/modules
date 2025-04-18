using System.Linq;
using JetBrains.Annotations;

namespace Shop
{
    [UsedImplicitly]
    public sealed class ShopItemModel : IShopContent
    {
        private readonly ShopItemConfig config;

        public IRequirement Requirement => config.Data.requirement;
        public IDescription Description => config.Data.description;

        public ShopItemModel(ShopItemConfig config)
        {
            this.config = config;
        }
        
        public bool TryGetGain<T>(out T gain) where T : IGain
        {
            gain = config.Data.gains.OfType<T>().FirstOrDefault();

            return gain != null;
        }

        public void Process(ShopResult result)
        {
            
        }
    }
}