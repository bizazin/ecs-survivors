using System;
using System.Collections.Generic;
using Code.Gameplay.Windows;
using Code.Meta.UI.GoldHolder.Service;
using Code.Meta.UI.Shop.Items;
using Code.Meta.UI.Shop.Service;
using Code.Meta.UI.Shop.UIFactory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.Shop
{
    public class ShopWindow: BaseWindow
    {
        public Transform ItemsLayout;
        public Button CloseButton;
        public GameObject NoItemsAvailable;
        
        public IWindowService _windowService;
        private IShopUIFactory _shopUIFactory;
        private IShopUIService _shop;

        private readonly List<ShopItem> _items = new();
        private IStorageUIService _storage;

        [Inject]
        private void Construct(IWindowService windowService, 
            IShopUIFactory shopUIFactory,
            IShopUIService shopUIService,
            IStorageUIService storage)
        {
            _storage = storage;
            Id = WindowId.ShopWindow;
            
            _shopUIFactory = shopUIFactory;
            _windowService = windowService;
            _shop = shopUIService;
        } 

        protected override void Initialize() 
            => CloseButton.onClick.AddListener(Close);

        protected override void SubscribeUpdates()
        {
            _shop.ShopChanged += Refresh;
            _storage.GoldBoostChanged += UpdateBoosterState;
            
            Refresh();
        }

        private void UpdateBoosterState()
        {
            bool itemsCanBeBought = Math.Abs(_storage.GoldGainBoost - 0) <= float.Epsilon;

            foreach (var shopItem in _items) 
                shopItem.UpdateAvailability(itemsCanBeBought);
        }

        protected override void UnsubscribeUpdates()
        {
            _shop.ShopChanged -= Refresh;
            _storage.GoldBoostChanged -= UpdateBoosterState;
        }

        private void Refresh()
        {
            ClearItems();
            
            List<ShopItemConfig> availableItems = _shop.GetAvailableShopItems;

            NoItemsAvailable.SetActive(availableItems.Count == 0);
            
            FillItems(availableItems);
            
            UpdateBoosterState();
        }

        private void ClearItems()
        {
            _items.ForEach(x=> Destroy(x.gameObject));
            _items.Clear();
        }

        private void FillItems(List<ShopItemConfig> availableItems)
        {
            foreach (var item in availableItems) 
                _items.Add(_shopUIFactory.CreateShopItem(item, ItemsLayout));
        }

        protected override void Cleanup()
        {
           base.Cleanup(); 
           
           CloseButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            _windowService.Close(Id);
        }
    }
}