using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class AnimateHeroMoveSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;

        public AnimateHeroMoveSystem(GameContext game)
        {
            _heroes = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Hero,
                GameMatcher.HeroAnimator));
        }

        public void Execute()
        {
            foreach (var hero in _heroes)
                if (hero.isMoving)
                    hero.HeroAnimator.PlayMove();
                else
                    hero.HeroAnimator.PlayIdle();
        }
    }
}