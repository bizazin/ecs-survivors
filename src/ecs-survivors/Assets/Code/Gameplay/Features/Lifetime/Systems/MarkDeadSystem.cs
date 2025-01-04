using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Lifetime.Systems
{
    public class MarkDeadSystem : IExecuteSystem
    {
        private readonly List<GameEntity> _buffer = new(128);
        private readonly IGroup<GameEntity> _entities;

        public MarkDeadSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.CurrentHp,
                    GameMatcher.MaxHp)
                .NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
                if (entity.CurrentHp <= 0)
                {
                    entity.isDead = true;
                    entity.isProcessingDeath = true;
                }
        }
    }
}