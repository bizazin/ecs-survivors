using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class SetHeroDirectionByInput : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _inputs;
        private readonly IGroup<GameEntity> _heroes;

        public SetHeroDirectionByInput(GameContext game)
        {
            _heroes = game.GetGroup(GameMatcher.Hero);
            _inputs = game.GetGroup(GameMatcher.Input);
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