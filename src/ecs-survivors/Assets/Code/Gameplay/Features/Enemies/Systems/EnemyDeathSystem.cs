using Code.Gameplay.Features.TargetCollection;
using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyDeathSystem : IExecuteSystem
    {
        private const float DeathAnimationTimeS = 2;
        private readonly IGroup<GameEntity> _enemies;

        public EnemyDeathSystem(GameContext game)
        {
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Dead,
                    GameMatcher.ProcessingDeath,
                    GameMatcher.Enemy));
        }

        public void Execute()
        {
            foreach (GameEntity enemy in _enemies)
            {
                enemy.isMovementAvailable = false;
                enemy.isTurnedAlongDirection = false;
                
                enemy.RemoveTargetCollectionComponents();
                
                if (enemy.hasEnemyAnimator)
                    enemy.EnemyAnimator.PlayDied();

                enemy.ReplaceSelfDestructTimer(DeathAnimationTimeS);
            }
        }
    }
}