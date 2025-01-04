using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Effects.Systems
{
    public class ProcessHealEffectSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;

        public ProcessHealEffectSystem(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.HealEffect,
                    GameMatcher.EffectValue,
                    GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (var effect in _effects)
            {
                var target = effect.Target();

                effect.isProcessed = true;

                if (target.isDead)
                    continue;

                if (target.hasCurrentHp && target.hasMaxHp)
                {
                    var newValue = Mathf.Min(target.CurrentHp + effect.EffectValue, target.MaxHp);
                    target.ReplaceCurrentHp(newValue);
                }
            }
        }
    }
}