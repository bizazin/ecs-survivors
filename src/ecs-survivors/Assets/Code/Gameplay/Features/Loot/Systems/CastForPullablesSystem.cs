using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CastForPullablesSystem : IExecuteSystem
    {
        private readonly GameEntity[] _hitBuffer = new GameEntity[128];
        private readonly int _layerMask = CollisionLayer.Collectable.AsMask();
        private readonly IGroup<GameEntity> _looters;
        private readonly IPhysicsService _physicsService;

        public CastForPullablesSystem(GameContext game, IPhysicsService physicsService)
        {
            _physicsService = physicsService;
            _looters = game.GetGroup(
                GameMatcher.AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PickupRadius));
        }

        public void Execute()
        {
            foreach (var entity in _looters)
            {
                for (var i = 0; i < LootinRadius(entity); i++)
                    if (_hitBuffer[i].isPullable)
                    {
                        _hitBuffer[i].isPullable = false;
                        _hitBuffer[i].isPulling = true;
                    }

                ClearBuffer();
            }
        }

        private void ClearBuffer()
        {
            for (var i = 0; i < _hitBuffer.Length; i++)
                _hitBuffer[i] = null;
        }

        private int LootinRadius(GameEntity entity)
        {
            return _physicsService.CircleCastNonAlloc(entity.WorldPosition, entity.PickupRadius, _layerMask,
                _hitBuffer);
        }
    }
}