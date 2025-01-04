using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.Cooldowns.Systems
{
    public class CooldownSystem : IExecuteSystem
    {
        private readonly List<GameEntity> _buffer = new(32);
        private readonly IGroup<GameEntity> _cooldownables;
        private readonly ITimeService _time;

        public CooldownSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _cooldownables = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Cooldown,
                    GameMatcher.CooldownLeft));
        }

        public void Execute()
        {
            foreach (var cooldownable in _cooldownables.GetEntities(_buffer))
            {
                cooldownable.ReplaceCooldownLeft(cooldownable.CooldownLeft - _time.DeltaTime);

                if (cooldownable.CooldownLeft <= 0)
                {
                    cooldownable.isCooldownUp = true;
                    cooldownable.RemoveCooldownLeft();
                }
            }
        }
    }
}