using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class CleanupUnappliedStatusLinkedChanges : ICleanupSystem
    {
        private readonly List<GameEntity> _buffer = new(32);
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _statuses;

        public CleanupUnappliedStatusLinkedChanges(GameContext game)
        {
            _game = game;
            _statuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.Status,
                    GameMatcher.Unapplied));
        }

        public void Cleanup()
        {
            foreach (var status in _statuses.GetEntities(_buffer))
            foreach (var entity in _game.GetEntitiesWithApplierStatusLink(status.Id))
                entity.isDestructed = true;
        }
    }
}