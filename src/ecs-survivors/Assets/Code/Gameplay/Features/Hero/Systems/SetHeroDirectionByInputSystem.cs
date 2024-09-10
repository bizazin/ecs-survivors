using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class SetHeroDirectionByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _inputs;
        private readonly IGroup<GameEntity> _heroes;

        public SetHeroDirectionByInputSystem(GameContext game)
        {
            _inputs = game.GetGroup(GameMatcher.Input);
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.MovementAvailable));
        }

        public void Execute()
        {
            foreach (var hero in _heroes)
            {
                foreach (var input in _inputs)
                {
                    hero.isMoving = input.hasAxisInput;

                    if (input.hasAxisInput) 
                        hero.ReplaceDirection(input.AxisInput.normalized);
                }
            }
        }
    }
}