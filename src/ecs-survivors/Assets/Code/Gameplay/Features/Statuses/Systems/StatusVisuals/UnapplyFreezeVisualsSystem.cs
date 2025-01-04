using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems.StatusVisuals
{
    public class UnapplyFreezeVisualsSystem : ReactiveSystem<GameEntity>
    {
        public UnapplyFreezeVisualsSystem(GameContext game) : base(game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(
                    GameMatcher.Freeze,
                    GameMatcher.Status,
                    GameMatcher.Unapplied)
                .Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStatus && entity.isFreeze && entity.hasTargetId;
        }

        protected override void Execute(List<GameEntity> statuses)
        {
            foreach (var status in statuses)
            {
                var target = status.Target();
                if (target is { hasStatusVisuals: true })
                    target.StatusVisuals.UnapplyFreeze();
            }
        }
    }
}