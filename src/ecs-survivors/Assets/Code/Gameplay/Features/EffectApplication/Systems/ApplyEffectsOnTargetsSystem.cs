using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.EffectApplication.Systems
{
    public class ApplyEffectsOnTargetsSystem : IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _entities;

        public ApplyEffectsOnTargetsSystem(GameContext game, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetBuffer,
                    GameMatcher.EffectSetups));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            foreach (var targetId in entity.TargetBuffer)
            foreach (var setup in entity.EffectSetups)
                _effectFactory.CreateEffect(setup, ProducerId(entity), targetId);
        }

        private static int ProducerId(GameEntity entity)
        {
            return entity.hasProducerId ? entity.ProducerId : entity.Id;
        }
    }
}