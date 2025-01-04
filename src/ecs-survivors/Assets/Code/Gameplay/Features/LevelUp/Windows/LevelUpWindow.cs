using System.Collections.Generic;
using Code.Common.Entity;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.LevelUp.Behaviours;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.LevelUp.Windows
{
    public class LevelUpWindow : BaseWindow
    {
        public Transform AbilityLayout;

        private readonly List<AbilityCard> _cards = new(3);
        private IAbilityUpgradeService _abilityUpgrade;
        private IAbilityUIFactory _factory;

        private IStaticDataService _staticData;
        private IWindowService _windowService;

        [Inject]
        private void Construct(
            IStaticDataService staticData,
            IAbilityUpgradeService abilityUpgrade,
            IWindowService windowService,
            IAbilityUIFactory abilityUIFactory)
        {
            Id = WindowId.LevelUpWindow;

            _staticData = staticData;
            _abilityUpgrade = abilityUpgrade;
            _windowService = windowService;
            _factory = abilityUIFactory;
        }

        protected override void Initialize()
        {
            foreach (var upgradeOption in _abilityUpgrade.GetUpgradeOptions())
            {
                var abilityLevel = _staticData.GetAbilityLevel(upgradeOption.Id, upgradeOption.Level);

                _cards.Add(
                    _factory.CreateAbilityCard(AbilityLayout)
                        .Setup(upgradeOption.Id, abilityLevel, OnSelected));
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