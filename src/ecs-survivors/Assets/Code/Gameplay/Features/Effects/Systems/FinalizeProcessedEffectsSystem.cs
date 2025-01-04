using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Effects.Systems
{
    public class CleanupProcessedEffectsSystem : ICleanupSystem
    {
        private readonly List<GameEntity> _buffer = new(32);
        private readonly IGroup<GameEntity> _effects;

        public CleanupProcessedEffectsSystem(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Effect,
                    GameMatcher.Processed));
        }

        public void Cleanup()
        {
            foreach (var effect in _effects.GetEntities(_buffer)) effect.Destroy();
        }
    }
}