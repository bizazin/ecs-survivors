using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class CleanupUnappliedStatuses : ICleanupSystem
    {
        private readonly List<GameEntity> _buffer = new(32);
        private readonly IGroup<GameEntity> _statuses;

        public CleanupUnappliedStatuses(GameContext game)
        {
            _statuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Status,
                    GameMatcher.Unapplied));
        }

        public void Cleanup()
        {
            foreach (var status in _statuses.GetEntities(_buffer)) status.isDestructed = true;
        }
    }
}