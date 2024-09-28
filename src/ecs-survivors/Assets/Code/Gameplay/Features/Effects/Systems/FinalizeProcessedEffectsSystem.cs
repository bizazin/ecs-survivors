using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Effects.Systems
{

    public class CleanupProcessedEffectsSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _effects;
        private readonly List<GameEntity> _buffer = new(32);

        public CleanupProcessedEffectsSystem(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Effect,
                    GameMatcher.Processed));
        }

        public void Cleanup()
        {
            foreach (GameEntity effect in _effects.GetEntities(_buffer))
            {
                effect.Destroy();
            }
        }
    }




}