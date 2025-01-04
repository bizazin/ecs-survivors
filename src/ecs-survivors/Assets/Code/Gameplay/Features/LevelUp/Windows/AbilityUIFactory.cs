using Code.Gameplay.Features.LevelUp.Behaviours;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.LevelUp.Windows
{
    public class AbilityUIFactory : IAbilityUIFactory
    {
        private const string AbilityCardPrefabPath = "UI/Abilities/AbilityCard";
        private readonly IAssetProvider _assetProvider;

        private readonly IInstantiator _instantiator;

        public AbilityUIFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public AbilityCard CreateAbilityCard(Transform parent)
        {
            return _instantiator.InstantiatePrefabForComponent<AbilityCard>(
                _assetProvider.LoadAsset(AbilityCardPrefabPath),
                parent);
        }
    }
}