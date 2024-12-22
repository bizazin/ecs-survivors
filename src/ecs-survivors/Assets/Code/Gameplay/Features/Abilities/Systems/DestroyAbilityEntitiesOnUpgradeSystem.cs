using Entitas;
using TMPro;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class DestroyAbilityEntitiesOnUpgradeSystem : IExecuteSystem
    {
        private GameContext _game;
        private readonly IGroup<GameEntity> _upgradeRequests;
        private readonly IGroup<GameEntity> _abilities;

        public DestroyAbilityEntitiesOnUpgradeSystem(GameContext game)
        {
            _game = game;
            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.AbilityId,
                    GameMatcher.RecreatedOnUpgrade));

            _upgradeRequests = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.UpgradeRequest,
                    GameMatcher.AbilityId));
        }

        public void Execute()
        {
            foreach (GameEntity request in _upgradeRequests)
            foreach (GameEntity ability in _abilities)
            {
                if (ability.AbilityId == request.AbilityId)
                {
                    foreach (var entity in _game.GetEntitiesWithParentAbility(ability.AbilityId))
                        entity.isDestructed = true;

                    ability.isActive = false;
                }
            }
        }
    }
}