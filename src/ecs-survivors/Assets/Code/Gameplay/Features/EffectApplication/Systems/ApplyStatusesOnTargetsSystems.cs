using Code.Common.Extensions;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Applier;
using Code.Gameplay.Features.Statuses.Factory;
using Entitas;

namespace Code.Gameplay.Features.EffectApplication.Systems
{
    public class ApplyStatusesOnTargetsSystems : IExecuteSystem
    {
        private readonly IStatusApplier _statusApplier;
        private readonly IGroup<GameEntity> _entities;

        public ApplyStatusesOnTargetsSystems(GameContext game, IStatusApplier statusApplier)
        {
            _statusApplier = statusApplier;

            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetsBuffer,
                    GameMatcher.StatusSetups));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            foreach (int targetId in entity.TargetsBuffer)
            foreach (StatusSetup setup in entity.StatusSetups)
            {
                _statusApplier.ApplyStatus(setup, ProducerId(entity), targetId);
            }
        }

        private int ProducerId(GameEntity entity) => 
            entity.hasProducerId ? entity.ProducerId : entity.Id;
    }
}