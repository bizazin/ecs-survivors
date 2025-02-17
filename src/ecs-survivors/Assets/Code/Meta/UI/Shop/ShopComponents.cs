using Code.Meta.UI.Shop.Items;
using Entitas;

namespace Code.Meta.UI.Shop
{
    public class ShopComponents
    {
        [Meta] public class ShopItemIdComponent : IComponent { public ShopItemId Value; }
        [Meta] public class Purchased : IComponent { }
        [Meta] public class BuyRequestComponent : IComponent { }

    }
}