using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class RemoveUnappliedEnchantsFromHolderSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _holders;

        public RemoveUnappliedEnchantsFromHolderSystem(GameContext game) : base(game)
        {
            _holders = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.EnchantsHolder));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(
                    GameMatcher.EnchantTypeId,
                    GameMatcher.Unapplied)
                .Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> enchants)
        {
            foreach (var holder in _holders)
            foreach (var enchant in enchants)
                holder.EnchantsHolder.RemoveEnchant(enchant.EnchantTypeId);
        }
    }
}