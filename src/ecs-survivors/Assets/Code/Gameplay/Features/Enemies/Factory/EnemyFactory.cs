using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Effects;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IIdentifierService _identifiers;

        public EnemyFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }

        public GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 at)
        {
            switch (typeId)
            {
                case EnemyTypeId.Goblin:
                    return CreateGoblin(at);
            }
            
            throw new Exception($"Enemy with typeId {typeId} does not exist");
        }

        private GameEntity CreateGoblin(Vector3 at) =>
            CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddEnemyTypeId(EnemyTypeId.Goblin)
                .AddWorldPosition(at)
                .AddDirection(Vector2.zero)
                .AddSpeed(1)
                .AddCurrentHP(3)
                .AddMaxHP(3)
                .AddEffectSetups(new List<EffectSetup>(){new(){EffectTypeId = EffectTypeId.Damage, Value = 1}})
                .AddTargetsBuffer(new List<int>(1))
                .AddRadius(0.3f)
                .AddCollectTargetsTimer(0)
                .AddCollectTargetsInterval(0.5f)
                .AddLayerMask(CollisionLayer.Hero.AsMask())
                .AddViewPath("Gameplay/Enemies/Goblins/Torch/goblin_torch_blue")
                .With(x => x.isEnemy = true)
                .With(x => x.isMovementAvailable = true)
                .With(x => x.isTurnedAlongDirection = true);
    }
}