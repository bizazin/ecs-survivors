using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class StatusDurationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statuses;
        private readonly ITimeService _timeService;

        public StatusDurationSystem(GameContext game, ITimeService timeService)
        {
            _timeService = timeService;
            _statuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Duration,
                    GameMatcher.Status,
                    GameMatcher.TimeLeft));
        }

        public void Execute()
        {
            foreach (var status in _statuses)
                if (status.TimeLeft >= 0)
                    status.ReplaceTimeLeft(status.TimeLeft - _timeService.DeltaTime);
                else
                    status.isUnapplied = true;
        }
    }
}