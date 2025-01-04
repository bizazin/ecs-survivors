using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CleanupCollectedSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _collected;

        public CleanupCollectedSystem(GameContext contextParameter)
        {
            _collected = contextParameter.GetGroup(GameMatcher.Collected);
        }

        public void Cleanup()
        {
            foreach (var collected in _collected)
                collected.isDestructed = true;
        }
    }
}