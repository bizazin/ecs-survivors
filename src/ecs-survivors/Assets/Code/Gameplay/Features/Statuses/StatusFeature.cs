using Code.Gameplay.Features.Statuses.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Statuses
{
    public class StatusFeature : Feature
    {
        public StatusFeature(ISystemFactory systems)
        {
            Add(systems.Create<StatusDurationSystem>());
            Add(systems.Create<PeriodicDamageStatusSystem>());
            Add(systems.Create<ApplyFreezeStatusSystem>());
            
            Add(systems.Create<StatusVisualsFeature>());

            Add(systems.Create<CleanupUnappliedStatusLinkedChangesSystem>());
            Add(systems.Create<CleanupUnappliedStatusesSystem>());
        }
    }
}