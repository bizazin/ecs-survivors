using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Lifetime.Systems
{
    public class FinalizeEnemyDeathProcessingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enemies;
        private readonly List<GameEntity> _buffer = new(128);

        public FinalizeEnemyDeathProcessingSystem(GameContext game) =>
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Dead,
                    GameMatcher.Enemy,
                    GameMatcher.ProcessingDeath));

        public void Execute()
        {
            foreach (GameEntity enemy in _enemies.GetEntities(_buffer))
                enemy.isProcessingDeath = false;
        }
    }
}