using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class StartTimeOnLevelUpProcessedSystem : ReactiveSystem<GameEntity>
    {
        private readonly ITimeService _timeService;

        public StartTimeOnLevelUpProcessedSystem(GameContext game, ITimeService timeService) : base(game)
        {
            _timeService = timeService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Processed.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isLevelUp && entity.isProcessed;
        }

        protected override void Execute(List<GameEntity> levelUps)
        {
            foreach (var _ in levelUps)
                _timeService.StartTime();
        }
    }
}