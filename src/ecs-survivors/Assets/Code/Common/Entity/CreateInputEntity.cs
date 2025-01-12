namespace Code.Common.Entity
{
    public static class CreateInputEntity
    {
        public static InputEntity Empty()
        {
            return Contexts.sharedInstance.input.CreateEntity();
        }
    }
}