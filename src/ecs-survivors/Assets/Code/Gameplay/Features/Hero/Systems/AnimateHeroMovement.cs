using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class AnimateHeroMovement : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;

        public AnimateHeroMovement(GameContext game)
        {
            _heroes = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Hero,
                GameMatcher.HeroAnimator));
        }

        public void Execute()
        {
            foreach (var hero in _heroes)
            {
                if (hero.isMoving)
                    hero.HeroAnimator.PlayMove();
                else
                    hero.HeroAnimator.PlayIdle();
            }
        }
    }
}