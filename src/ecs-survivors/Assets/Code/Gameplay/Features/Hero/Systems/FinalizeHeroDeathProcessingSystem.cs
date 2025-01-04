using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class FinalizeHeroDeathProcessingSystem : IExecuteSystem
    {
        private readonly List<GameEntity> _buffer = new(1);
        private readonly IGroup<GameEntity> _heroes;

        public FinalizeHeroDeathProcessingSystem(GameContext game)
        {
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.Dead,
                    GameMatcher.ProcessingDeath));
        }

        public void Execute()
        {
            foreach (var hero in _heroes.GetEntities(_buffer)) hero.isProcessingDeath = false;
        }
    }
}