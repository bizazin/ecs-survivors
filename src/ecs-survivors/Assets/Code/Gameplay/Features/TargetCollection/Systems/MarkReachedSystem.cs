using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class MarkReachedSystem : IExecuteSystem
    {
        private readonly List<GameEntity> _buffer = new(128);
        private readonly IGroup<GameEntity> _entities;

        public MarkReachedSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.TargetBuffer)
                .NoneOf(GameMatcher.Reached));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
                if (entity.TargetBuffer.Count > 0)
                    entity.isReached = true;
        }
    }
}