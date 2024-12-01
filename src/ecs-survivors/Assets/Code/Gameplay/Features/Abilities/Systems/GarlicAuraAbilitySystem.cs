using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class GarlicAuraAbilitySystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;
        private readonly IGroup<GameEntity> _abilities;
        private readonly List<GameEntity> _buffer = new(1);
        private readonly IGroup<GameEntity> _heroes;

        public GarlicAuraAbilitySystem(GameContext game, IStaticDataService staticDataService,
            IArmamentFactory armamentFactory)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;
            _abilities = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.GarlicAuraAbility)
                .NoneOf(GameMatcher.Active));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            foreach (GameEntity hero in _heroes)
            {
                _armamentFactory.CreateEffectAura(AbilityId.GarlicAura, hero.Id, level: 1);

                ability
                    .isActive = true;
            } 
        }
    }
}