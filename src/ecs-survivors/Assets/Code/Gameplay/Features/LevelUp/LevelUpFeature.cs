using Code.Gameplay.Features.LevelUp.Systems;
using Code.Infrastructure.Systems;
using Unity.VisualScripting;

namespace Code.Gameplay.Features.LevelUp
{
    public class LevelUpFeature : Feature
    {
        public LevelUpFeature(ISystemFactory systems)
        {
            Add(systems.Create<OpenLevelUpWindowSystem>());
            Add(systems.Create<StopTimeOnLevelUpSystem>());
            
            Add(systems.Create<UpgradeAbilityOnRequestSystem>());
            
            Add(systems.Create<StartTimeOnLevelUpProcessedSystem>());

            Add(systems.Create<FinalizeProcessedLevelUpsSystem>());
        }
    }
}