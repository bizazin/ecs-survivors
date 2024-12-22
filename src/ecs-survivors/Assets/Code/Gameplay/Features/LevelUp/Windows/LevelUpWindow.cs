using Code.Common.Entity;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.LevelUp.Windows
{
    public class LevelUpWindow : BaseWindow
    {
        public Transform AbilityLayout;
        private IAbilityUpgradeService _abilityUpgradeService;
        private IAbilityUIFactory _factory;
        private IStaticDataService _staticData;
        private IWindowService _windowService;

        [Inject]
        private void Construct
        (
            IAbilityUIFactory factory,
            IAbilityUpgradeService abilityUpgradeService,
            IStaticDataService staticData,
            IWindowService windowService
        )
        {
            _windowService = windowService;
            _staticData = staticData;
            Id = WindowId.LevelUpWindow;
            
            _factory = factory;
            _abilityUpgradeService = abilityUpgradeService;
        }

        protected override void Initialize()
        {
            foreach (AbilityUpgradeOption upgradeOption in _abilityUpgradeService.GetUpgradeOptions())
            {
                AbilityLevel abilityLevel = _staticData.GetAbilityLevel(upgradeOption.Id, upgradeOption.Level);

                _factory.CreateAbilityCard(AbilityLayout).Setup(upgradeOption.Id, abilityLevel, OnSelected);
            }
        }

        private void OnSelected(AbilityId id)
        {
            CreateEntity.Empty()
                .AddAbilityId(id)
                .isUpgradeRequest = true;
            
            _windowService.Close(Id);
        }
    }
}