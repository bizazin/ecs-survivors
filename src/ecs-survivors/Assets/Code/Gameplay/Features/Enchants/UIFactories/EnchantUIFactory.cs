using Code.Gameplay.Features.Enchants.Behaviours;
using Code.Gameplay.StaticData;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Enchants.UIFactories
{
    public class EnchantUIFactory : IEnchantUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private const string EnchantPrefabPath = "UI/Enchants/Enchant";

        public EnchantUIFactory(IInstantiator instantiator, IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public Enchant CreateEnchant(Transform parent, EnchantTypeId typeId)
        {
            EnchantConfig config = _staticData.GetEnchantConfig(typeId);
            Enchant enchant = _instantiator.InstantiatePrefabForComponent<Enchant>(_assetProvider.LoadAsset<Enchant>(EnchantPrefabPath), parent);
            
            enchant.Set(config);

            return enchant;
        }
    }
}