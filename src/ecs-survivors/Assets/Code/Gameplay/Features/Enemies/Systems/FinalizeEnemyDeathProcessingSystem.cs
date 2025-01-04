using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class FinalizeEnemyDeathProcessingSystem : IExecuteSystem
    {
        private readonly List<GameEntity> _buffer = new(128);
        private readonly IGroup<GameEntity> _enemies;

        public FinalizeEnemyDeathProcessingSystem(GameContext game)
        {
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.Dead,
                    GameMatcher.ProcessingDeath));
        }

        public void Execute()
        {
            foreach (var enemy in _enemies.GetEntities(_buffer)) enemy.isProcessingDeath = false;
        }
    }
}