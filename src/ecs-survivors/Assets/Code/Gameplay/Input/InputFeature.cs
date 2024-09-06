using Code.Gameplay.Input.Service;
using Code.Gameplay.Input.Systems;

namespace Code.Gameplay.Input
{
    public class InputFeature : Feature
    {
        public InputFeature(GameContext gameContext, IInputService inputService)
        {
            Add(new InputInitializeSystem());
            Add(new EmitInputSystem(gameContext, inputService));
        }
    }
}