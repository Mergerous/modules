using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Shop
{
    [UsedImplicitly]
    public sealed class ShopModel
    {
        private readonly ShopConfigSo config;

        public ShopModel(ShopConfigSo config)
        {
            this.config = config;
        }

        public IEnumerable<IShopContent> ItemModels => config.Configs.Select(itemConfig => new ShopItemModel(itemConfig));
    }
}