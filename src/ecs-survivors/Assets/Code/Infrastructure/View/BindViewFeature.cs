using Code.Infrastructure.Systems;
using Code.Infrastructure.View.Systems;

namespace Code.Infrastructure.View
{
    public class BindViewFeature : Feature
    {
        public BindViewFeature(ISystemFactory systems)
        {
            Add(systems.Create<BindEntityViewFromPrefabSystem>());
            Add(systems.Create<BindEntityViewFromPathSystem>());
        }
    }
}