﻿using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Abilities.System
{
    public class VegetableBoltAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _abilities;
        private readonly IAbilityUpgradeService _abilityUpgradeService;

        private readonly IArmamentFactory _armamentFactory;
        private readonly List<GameEntity> _buffer = new(4);
        private readonly IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IStaticDataService _staticDataService;

        public VegetableBoltAbilitySystem(
            GameContext game,
            IArmamentFactory armamentFactory,
            IAbilityUpgradeService abilityUpgradeService,
            IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;
            _abilityUpgradeService = abilityUpgradeService;

            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.VegetableBoltAbility,
                    GameMatcher.CooldownUp));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));

            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var ability in _abilities.GetEntities(_buffer))
            foreach (var hero in _heroes)
            {
                if (_enemies.count <= 0)
                    continue;

                var level = _abilityUpgradeService.GetAbilityLevel(AbilityId.VegetableBolt);

                _armamentFactory
                    .CreateVegetableBolt(1, hero.WorldPosition)
                    .AddProducerId(hero.Id)
                    .ReplaceDirection((FirstAvailableTarget().WorldPosition - hero.WorldPosition).normalized)
                    .With(x => x.isMoving = true);

                ability
                    .PutOnCooldown(_staticDataService.GetAbilityLevel(AbilityId.VegetableBolt, level).Cooldown);
            }
        }

        private GameEntity FirstAvailableTarget()
        {
            return _enemies.AsEnumerable().First();
        }
    }
}