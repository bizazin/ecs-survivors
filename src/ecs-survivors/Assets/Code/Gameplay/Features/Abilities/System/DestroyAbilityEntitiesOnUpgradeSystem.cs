using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class DestroyAbilityEntitiesOnUpgradeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _abilities;

        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _upgradeRequests;

        public DestroyAbilityEntitiesOnUpgradeSystem(GameContext game)
        {
            _game = game;
            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.RecreatedOnUpgrade,
                    GameMatcher.AbilityId));

            _upgradeRequests = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.UpgradeRequest,
                    GameMatcher.AbilityId));
        }

        public void Execute()
        {
            foreach (var request in _upgradeRequests)
            foreach (var ability in _abilities)
                if (request.AbilityId == ability.AbilityId)
                {
                    foreach (var entity in _game.GetEntitiesWithParentAbility(ability.AbilityId))
                        entity.isDestructed = true;

                    ability.isActive = false;
                }
        }
    }
}