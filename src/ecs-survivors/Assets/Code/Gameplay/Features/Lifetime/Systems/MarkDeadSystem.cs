using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Lifetime.Systems
{
    public class MarkDeadSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enemies;
        private readonly List<GameEntity> _buffer = new(128);

        public MarkDeadSystem(GameContext game) =>
            _enemies = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.CurrentHP)
                .NoneOf(GameMatcher.Dead));

        public void Execute()
        {
            foreach (GameEntity entity in _enemies.GetEntities(_buffer))
                if (entity.CurrentHP <= 0)
                {
                    entity.isDead = true;
                    entity.isProcessingDeath = true;
                }
        }
    }
}