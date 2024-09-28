using Code.Gameplay.Features.EffectApplication.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.EffectApplication
{
    public class EffectApplicationFeature : Feature
    {
        public EffectApplicationFeature(ISystemFactory systems)
        {
            Add(systems.Create<ApplyEffectsOnTargetsSystems>());
            Add(systems.Create<ApplyStatusesOnTargetsSystems>());
        }
    }
}