using System.Collections.Generic;
using Entitas;

namespace Code.Common.Destruct.Systems
{
    public class CleanupMetaDestructedSystem : ICleanupSystem
    {
        private readonly List<MetaEntity> _buffer = new(16);
        private readonly IGroup<MetaEntity> _entities;

        public CleanupMetaDestructedSystem(MetaContext metaContext)
        {
            _entities = metaContext.GetGroup(MetaMatcher.Destructed);
        }

        public void Cleanup()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
                entity.Destroy();
        }
    }
}