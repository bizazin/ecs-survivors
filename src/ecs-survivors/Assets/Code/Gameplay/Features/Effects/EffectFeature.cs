using Code.Gameplay.Features.Effects.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Effects
{
    public sealed class EffectFeature : Feature
    {
        public EffectFeature(ISystemFactory systems)
        {
            Add(systems.Create<CleanupProcessedEffects>());
            
            Add(systems.Create<RemoveEffectsWithoutTargetsSystem>());

            Add(systems.Create<ProcessDamageEffectSystem>());
            Add(systems.Create<ProcessHealEffectSystem>());

        }
    }
}