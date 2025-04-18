using Code.Gameplay.Features.Statuses.Applier;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectStatusItemSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _collected;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IStatusApplier _statusApplier;

        public CollectStatusItemSystem(GameContext game, IStatusApplier statusApplier)
        {
            _statusApplier = statusApplier;
            _collected = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Collected,
                    GameMatcher.StatusSetups));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var loot in _collected)
            foreach (var hero in _heroes)
            foreach (var statusSetup in loot.StatusSetups)
                _statusApplier.ApplyStatus(statusSetup, hero.Id, hero.Id);
        }
    }
}