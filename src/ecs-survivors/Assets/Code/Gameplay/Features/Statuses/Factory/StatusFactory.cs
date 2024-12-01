using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Enchants;
using Code.Infrastructure.Identifiers;
using Unity.VisualScripting;

namespace Code.Gameplay.Features.Statuses.Factory
{
    public class StatusFactory : IStatusFactory
    {
        private readonly IIdentifierService _identifiers;

        public StatusFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }

        public GameEntity CreateStatus(StatusSetup setup, int producerId, int targetId)
        {
            GameEntity status;

            switch (setup.StatusTypeId)
            {
                case StatusTypeId.Poison:
                    status = CreatePoisonStatus(setup, producerId, targetId);
                    break;
                case StatusTypeId.Freeze:
                    status = CreateFreezeStatus(setup, producerId, targetId);
                    break;
                case StatusTypeId.PoisonEnchant:
                    status = CreatePoisonEnchantStatus(setup, producerId, targetId);
                    break;
                case StatusTypeId.ExplosiveEnchant:
                    status = CreateExplosiveEnchantStatus(setup, producerId, targetId);
                    break;
                default:
                    throw new Exception($"Status with type id {setup.StatusTypeId} does not exist");
            }

            status
                .With(x => x.AddDuration(setup.Duration), when:setup.Duration > 0)
                .With(x => x.AddTimeLeft(setup.Duration), when:setup.Duration > 0)
                .With(x => x.AddPeriod(setup.Period), when:setup.Period > 0)
                .With(x => x.AddTimeSinceLastTick(0), when:setup.Period > 0)
                ;
            return status;
        }

        private GameEntity CreatePoisonStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddStatusTypeId(setup.StatusTypeId)
                .AddEffectValue(setup.Value)
                .With(x => x.isStatus = true)
                .With(x => x.isPoison = true)
                .AddProducerId(producerId)
                .AddTargetId(targetId);
        }
        
        private GameEntity CreateFreezeStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddStatusTypeId(setup.StatusTypeId)
                .AddEffectValue(setup.Value)
                .With(x => x.isStatus = true)
                .With(x => x.isFreeze = true)
                .AddProducerId(producerId)
                .AddTargetId(targetId);
        }
        
        private GameEntity CreatePoisonEnchantStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddStatusTypeId(StatusTypeId.PoisonEnchant)
                .AddEnchantTypeId(EnchantTypeId.PoisonArmaments)
                .AddEffectValue(setup.Value)
                .With(x => x.isStatus = true)
                .With(x => x.isPoisonEnchant = true)
                .AddProducerId(producerId)
                .AddTargetId(targetId);
        }
        
        private GameEntity CreateExplosiveEnchantStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddStatusTypeId(StatusTypeId.ExplosiveEnchant)
                .AddEnchantTypeId(EnchantTypeId.ExplosiveArmaments)
                .AddEffectValue(setup.Value)
                .With(x => x.isStatus = true)
                .With(x => x.isExplosiveEnchant = true)
                .AddProducerId(producerId)
                .AddTargetId(targetId);
        }
    }
}