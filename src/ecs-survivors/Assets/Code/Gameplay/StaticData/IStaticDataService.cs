using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        AbilityConfig GetAbilityById(AbilityId abilityId);
        AbilityLevel GetAbilityLevel(AbilityId abilityId, int level);
        EnchantConfig GetEnchantConfig(EnchantTypeId enchantId);
    }
}