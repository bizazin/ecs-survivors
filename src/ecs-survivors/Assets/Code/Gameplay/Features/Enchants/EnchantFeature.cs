using Code.Gameplay.Features.Enchants.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Enchants
{
    public class EnchantFeature : Feature
    {
        public EnchantFeature(ISystemFactory systems)
        {
            Add(systems.Create<PoisonEnchantSystem>());
            Add(systems.Create<ExplosiveEnchantSystem>());
            
            Add(systems.Create<AddEnchantsToHolderSystem>());
            Add(systems.Create<ApplyPoisonEnchantVisualsSystem>());
        }
    }
}