using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Physics;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CastForTargetsWithLimitSystem : IExecuteSystem, ITearDownSystem
    {
        private readonly IPhysicsService _physicsService;
        private readonly IGroup<GameEntity> _ready;
        private readonly List<GameEntity> _buffer = new (64);
        private GameEntity[] _targetCastBuffer = new GameEntity[128];

        public CastForTargetsWithLimitSystem(GameContext game, IPhysicsService physicsService)
        {
            _physicsService = physicsService;
            _ready = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetsBuffer,
                    GameMatcher.Radius, 
                    GameMatcher.LayerMask,
                    GameMatcher.WorldPosition,
                    GameMatcher.ReadyToCollectTargets,
                    GameMatcher.TargetLimit));
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _ready.GetEntities(_buffer))
            {
                for (int i = 0; i < Math.Min(TargetCountInRadius(entity), entity.TargetLimit); i++)
                {   
                    var targetId = _targetCastBuffer[i].Id;

                    if (!AlreadyProcessed(entity, targetId))
                    {
                        entity.TargetsBuffer.Add(targetId);
                        entity.ProcessedTargets.Add(targetId);
                    }
                }
                
                if (!entity.isCollectingTargetsContinuously)
                    entity.isReadyToCollectTargets = false;
            }
        }

        private bool AlreadyProcessed(GameEntity entity, int targetId) => 
            entity.ProcessedTargets.Contains(targetId);

        private int TargetCountInRadius(GameEntity entity) => 
            _physicsService
                .CircleCastNonAlloc(entity.WorldPosition, entity.Radius, entity.LayerMask, _targetCastBuffer);

        public void TearDown()
        {
            _targetCastBuffer = null;
        }
    }
}