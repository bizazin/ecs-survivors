﻿using Code.Gameplay.Input.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Input
{
    public class InputFeature : Feature
    {
        public InputFeature(ISystemFactory systems)
        {
            Add(systems.Create<InputInitializeSystem>());
            Add(systems.Create<EmitInputSystem>());
        }
    }
}