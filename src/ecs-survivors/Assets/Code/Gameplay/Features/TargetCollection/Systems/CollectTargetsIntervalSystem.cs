using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CollectTargetsIntervalSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly ITimeService _time;

        public CollectTargetsIntervalSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetBuffer,
                    GameMatcher.CollectTargetsInterval,
                    GameMatcher.CollectTargetsTimer));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                entity.ReplaceCollectTargetsTimer(entity.CollectTargetsTimer - _time.DeltaTime);

                if (entity.CollectTargetsTimer <= 0)
                {
                    entity.isReadyToCollectTargets = true;
                    entity.ReplaceCollectTargetsTimer(entity.CollectTargetsInterval);
                }
            }
        }
    }
}