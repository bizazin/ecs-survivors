using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CleanupTargetsBuffersSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public CleanupTargetsBuffersSystem(GameContext game) => 
            _entities = game.GetGroup(GameMatcher.TargetsBuffer);

        public void Cleanup()
        {
            foreach (GameEntity entity in _entities) 
                entity.TargetsBuffer.Clear();
        }
    }
}