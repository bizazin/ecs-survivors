using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyChaseHeroSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;

        public EnemyChaseHeroSystem(GameContext game)
        {
            _heroes = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Hero,
                GameMatcher.WorldPosition));

            _enemies = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Enemy,
                GameMatcher.WorldPosition));
        }
        
        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                foreach (GameEntity enemy in _enemies)
                {
                    enemy.ReplaceDirection((hero.WorldPosition - enemy.WorldPosition).normalized);
                    enemy.isMoving = true;
                }
            }
        }
    }
}