using Code.Gameplay.Features.Movement.Systems;
using Code.Infrastructure.Systems;
using UnityEngine.UIElements;

namespace Code.Gameplay.Features.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory systems)
        {
            Add(systems.Create<DirectionalDeltaMoveSystem>());
            
            Add(systems.Create<TurnAlongDirectionSystem>());
            Add(systems.Create<UpdateTransformPositionSystem>());
            Add(systems.Create<RotateAlongDirectionSystem>());
        }
    }
}