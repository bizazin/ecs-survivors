using System.Collections.Generic;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Entitas;

namespace Code.Gameplay.Features.Abilities.System
{
    public class GarlicAuraAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _abilities;
        private readonly IAbilityUpgradeService _abilityUpgradeService;

        private readonly IArmamentFactory _armamentFactory;
        private readonly List<GameEntity> _buffer = new(1);
        private readonly IGroup<GameEntity> _heroes;

        public GarlicAuraAbilitySystem(GameContext game, IArmamentFactory armamentFactory,
            IAbilityUpgradeService abilityUpgradeService)
        {
            _armamentFactory = armamentFactory;
            _abilityUpgradeService = abilityUpgradeService;
            _abilities = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.GarlicAuraAbility)
                .NoneOf(GameMatcher.Active));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.Hero));
        }

        public void Execute()
        {
            foreach (var ability in _abilities.GetEntities(_buffer))
            foreach (var hero in _heroes)
            {
                var level = _abilityUpgradeService.GetAbilityLevel(AbilityId.GarlicAura);
                _armamentFactory.CreateEffectAura(AbilityId.GarlicAura, hero.Id, level);

                ability.isActive = true;
            }
        }
    }
}