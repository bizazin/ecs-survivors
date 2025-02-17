﻿using Code.Common.Entity;
using Entitas;

namespace Code.Gameplay.Input.Systems
{
    public class InputInitializeSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateInputEntity.Empty()
                .isInput = true;
        }
    }
}