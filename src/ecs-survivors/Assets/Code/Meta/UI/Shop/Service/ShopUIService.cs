using System;
using System.Collections.Generic;
using Code.Gameplay.StaticData;
using Code.Meta.UI.Shop.Items;
using Unity.VisualScripting;

namespace Code.Meta.UI.Shop.Service
{
    public class ShopUIService : IShopUIService
    {
        private List<ShopItemId> _purchasedItems = new();
        private Dictionary<ShopItemId, ShopItemConfig> _availableItems = new();
        private readonly IStaticDataService _staticData;
        private List<ShopItemConfig> _getAvailableShopItems;

        public event Action ShopChanged;
        
        public ShopUIService(IStaticDataService staticData) 
            => _staticData = staticData;

        public void UpdatePurchasedItems(IEnumerable<ShopItemId> purchasedItems)
        {
            _purchasedItems.AddRange(purchasedItems);

            RefreshAvailableItems();
        }

        public ShopItemConfig GetConfig(ShopItemId shopItemId) 
            => _availableItems.GetValueOrDefault(shopItemId);

        public List<ShopItemConfig> GetAvailableShopItems
            => new(_availableItems.Values);

        public void Cleanup()
        {
              _purchasedItems.Clear();
              _availableItems.Clear();

              ShopChanged = null;
        }

        public void UpdatePurchasedItem(ShopItemId shopItemId)
        {
            _availableItems.Remove(shopItemId);
            _purchasedItems.Add(shopItemId);
            
            ShopChanged?.Invoke();
        }


        private void RefreshAvailableItems()
        {
            var shopItemConfigs = _staticData.GetShopItemConfigs();
            foreach (var itemConfig in shopItemConfigs)
            {
                if(!_purchasedItems.Contains(itemConfig.ShopItemId))
                    _availableItems.Add(itemConfig.ShopItemId, itemConfig);
            }
            
            ShopChanged?.Invoke();
        }
    }
}