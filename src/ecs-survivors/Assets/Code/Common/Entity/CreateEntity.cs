namespace Code.Common.Entity
{
    public static class CreateEntity
    {
        public static GameEntity Empty()
        {
            return Contexts.sharedInstance.game.CreateEntity();
        }
    }
}