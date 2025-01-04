using Code.Gameplay.Features.Statuses.Applier;
using Entitas;

namespace Code.Gameplay.Features.EffectApplication.Systems
{
    public class ApplyStatusesOnTargetsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly IStatusApplier _statusApplier;

        public ApplyStatusesOnTargetsSystem(GameContext game, IStatusApplier statusApplier)
        {
            _statusApplier = statusApplier;
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetBuffer,
                    GameMatcher.StatusSetups));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            foreach (var targetId in entity.TargetBuffer)
            foreach (var setup in entity.StatusSetups)
                _statusApplier.ApplyStatus(setup, ProducerId(entity), targetId);
        }

        private static int ProducerId(GameEntity entity)
        {
            return entity.hasProducerId ? entity.ProducerId : entity.Id;
        }
    }
}