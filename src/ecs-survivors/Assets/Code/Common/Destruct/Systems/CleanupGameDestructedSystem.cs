using System.Collections.Generic;
using Entitas;

namespace Code.Common.Destruct.Systems
{
    public class CleanupGameDestructedSystem : ICleanupSystem
    {
        private readonly List<GameEntity> _buffer = new(64);
        private readonly IGroup<GameEntity> _entities;

        public CleanupGameDestructedSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.Destructed);
        }

        public void Cleanup()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
                entity.Destroy();
        }
    }
}