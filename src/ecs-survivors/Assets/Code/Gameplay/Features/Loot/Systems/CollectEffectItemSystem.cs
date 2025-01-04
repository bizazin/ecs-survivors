using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectEffectItemSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _collected;
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _heroes;

        public CollectEffectItemSystem(GameContext game, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _collected = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Collected,
                    GameMatcher.EffectSetups));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var loot in _collected)
            foreach (var hero in _heroes)
            foreach (var effectSetup in loot.EffectSetups)
                _effectFactory.CreateEffect(effectSetup, hero.Id, hero.Id);
        }
    }
}