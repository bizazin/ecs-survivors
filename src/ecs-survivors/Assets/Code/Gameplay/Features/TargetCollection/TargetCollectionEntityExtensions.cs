namespace Code.Gameplay.Features.TargetCollection
{
    public static class TargetCollectionEntityExtensions
    {
        public static GameEntity RemoveTargetCollectionComponents(this GameEntity entity)
        {
            if (entity.hasTargetsBuffer)
                entity.RemoveTargetsBuffer();
            
            if (entity.hasCollectTargetsTimer)
                entity.RemoveCollectTargetsTimer();
            
            if (entity.hasCollectTargetsInterval)
                entity.RemoveCollectTargetsInterval();
            
            entity.isReadyToCollectTargets = false;
            
            return entity;
        }
    }
}