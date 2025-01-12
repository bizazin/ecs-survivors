namespace Code.Common.Entity
{
    public static class CreateMetaEntity
    {
        public static MetaEntity Empty()
        {
            return Contexts.sharedInstance.meta.CreateEntity();
        }
    }
}