using System.Collections.Generic;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Abilities.System
{
    public class OrbitingMushroomAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _abilities;
        private readonly IAbilityUpgradeService _abilityUpgradeService;
        private readonly IArmamentFactory _armamentFactory;

        private readonly List<GameEntity> _buffer = new(1);
        private readonly IGroup<GameEntity> _heroes;
        private readonly IStaticDataService _staticDataService;

        public OrbitingMushroomAbilitySystem(
            GameContext game,
            IArmamentFactory armamentFactory,
            IStaticDataService staticDataService,
            IAbilityUpgradeService abilityUpgradeService)
        {
            _staticDataService = staticDataService;
            _abilityUpgradeService = abilityUpgradeService;
            _armamentFactory = armamentFactory;

            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.OrbitingMushroomAbility,
                    GameMatcher.CooldownUp));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var ability in _abilities.GetEntities(_buffer))
            foreach (var hero in _heroes)
            {
                var level = _abilityUpgradeService.GetAbilityLevel(AbilityId.OrbitingMushroom);
                var abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.OrbitingMushroom, level);

                var projectileCount = abilityLevel.ProjectileSetup.ProjectileCount;

                for (var i = 0; i < projectileCount; i++)
                {
                    var phase = 2 * Mathf.PI * i / projectileCount;

                    CreateProjectile(hero, phase, level);
                }

                ability.PutOnCooldown(abilityLevel.Cooldown);
            }
        }

        private void CreateProjectile(GameEntity hero, float phase, int level)
        {
            _armamentFactory.CreateMushroom(level, hero.WorldPosition + Vector3.up, phase)
                .AddProducerId(hero.Id)
                .AddOrbitCenterPosition(hero.WorldPosition)
                .AddOrbitCenterFollowTarget(hero.Id)
                .isMoving = true;
        }
    }
}