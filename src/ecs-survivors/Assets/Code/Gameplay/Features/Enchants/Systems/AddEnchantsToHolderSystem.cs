using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class AddEnchantsToHolderSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enсhants;
        private readonly IGroup<GameEntity> _holders;

        public AddEnchantsToHolderSystem(GameContext game)
        {
            _holders = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.EnchantsHolder));

            _enсhants = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.EnchantTypeId,
                    GameMatcher.TimeLeft));
        }

        public void Execute()
        {
            foreach (var holder in _holders)
            foreach (var enchant in _enсhants)
                holder.EnchantsHolder.AddEnchant(enchant.EnchantTypeId);
        }
    }
}