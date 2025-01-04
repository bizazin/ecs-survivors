using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class PeriodicDamageStatusSystem : IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _statuses;
        private readonly ITimeService _timeService;

        public PeriodicDamageStatusSystem(GameContext game, ITimeService timeService, IEffectFactory effectFactory)
        {
            _timeService = timeService;
            _effectFactory = effectFactory;
            _statuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Status,
                    GameMatcher.Period,
                    GameMatcher.TimeSinceLastTick,
                    GameMatcher.EffectValue,
                    GameMatcher.ProducerId,
                    GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (var status in _statuses)
                if (status.TimeSinceLastTick >= 0)
                {
                    status.ReplaceTimeSinceLastTick(status.TimeSinceLastTick - _timeService.DeltaTime);
                }
                else
                {
                    status.ReplaceTimeSinceLastTick(status.Period);

                    _effectFactory.CreateEffect(
                        new EffectSetup { EffectTypeId = EffectTypeId.Damage, Value = status.EffectValue },
                        status.ProducerId,
                        status.TargetId);
                }
        }
    }
}