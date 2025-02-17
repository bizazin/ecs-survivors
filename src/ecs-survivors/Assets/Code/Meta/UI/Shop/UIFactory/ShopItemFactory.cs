using System;
using Code.Common.Entity;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using Code.Meta.UI.Shop.Items;

namespace Code.Meta.UI.Shop.UIFactory
{
    public class ShopItemFactory : IShopItemFactory
    {
        private readonly IIdentifierService _identifiers;
        private readonly IStaticDataService _staticData;

        public ShopItemFactory(IStaticDataService staticData, IIdentifierService identifiers)
        {
            _staticData = staticData;
            _identifiers = identifiers;
        }

        public MetaEntity CreateShopItem(ShopItemId shopItemId)
        {
            var config = _staticData.GetShopItemConfig(shopItemId);

            switch (config.Kind)
            {
                case ShopItemKind.Booster:
                    return CreateMetaEntity.Empty()
                        .AddId(_identifiers.Next())
                        .AddGoldGainBoost(config.Boost)
                        .AddDuration(config.Duration);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}