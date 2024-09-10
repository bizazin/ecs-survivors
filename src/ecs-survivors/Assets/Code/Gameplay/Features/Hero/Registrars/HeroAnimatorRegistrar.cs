using Code.Gameplay.Features.Hero.Behaviours;
using Code.Infrastructure.Views.Registrars;

namespace Code.Gameplay.Features.Hero.Registrars
{
    public class HeroAnimatorRegistrar : EntityComponentRegistrar
    {
        public HeroAnimator HeroAnimator;

        public override void RegisterComponents()
        {
            Entity
                .AddDamageTakenAnimator(HeroAnimator)
                .AddHeroAnimator(HeroAnimator);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasHeroAnimator)
                Entity.RemoveHeroAnimator();
            
            if (Entity.hasDamageTakenAnimator)
                Entity.RemoveDamageTakenAnimator();
        }
    }
}