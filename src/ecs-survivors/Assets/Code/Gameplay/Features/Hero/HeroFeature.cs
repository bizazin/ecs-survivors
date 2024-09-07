using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Cameras.Systems;
using Code.Gameplay.Features.Hero.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Hero
{
    public class HeroFeature : Feature
    {
        public HeroFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetHeroDirectionByInput>());
            Add(systems.Create<CameraFollowHeroSystem>());
            Add(systems.Create<AnimateHeroMovement>());
        }
    }
}