using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class RemoveUnappliedEnchantsFromHolder : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _holders;

        public RemoveUnappliedEnchantsFromHolder(GameContext game) : base(game)
        {
            _holders = game.GetGroup(GameMatcher.EnchantHolder);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher
                .AllOf(
                    GameMatcher.EnchantTypeId,
                    GameMatcher.Unapplied)
                .Added());

        protected override bool Filter(GameEntity entity) => true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity enchant in entities)
            foreach (GameEntity holder in _holders)
                holder.EnchantHolder.RemoveEnchant(enchant.EnchantTypeId);
        }
    }
}