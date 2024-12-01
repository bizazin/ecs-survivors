using Code.Gameplay.Features.Effects.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Effects
{
    public class EffectFeature : Feature
    {
        public EffectFeature(ISystemFactory systems)
        {
            Add(systems.Create<RemoveEffectsWithoutTargetsSystem>());
            
            Add(systems.Create<ProcessDamageEffectSystem>());
            Add(systems.Create<ProcessHealEffectSystem>());
            
            Add(systems.Create<CleanupProcessedEffectsSystem>());
        }
    }


}