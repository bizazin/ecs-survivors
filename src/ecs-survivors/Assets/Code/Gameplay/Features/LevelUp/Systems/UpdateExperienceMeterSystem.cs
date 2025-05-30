﻿using Code.Gameplay.Features.LevelUp.Services;
using Entitas;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class UpdateExperienceMeterSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _experienceMeters;
        private readonly IGroup<GameEntity> _heroes;
        private readonly ILevelUpService _levelUpService;

        public UpdateExperienceMeterSystem(GameContext game, ILevelUpService levelUpService)
        {
            _levelUpService = levelUpService;
            _experienceMeters = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ExperienceMeter));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.Experience));
        }

        public void Execute()
        {
            foreach (var experienceMeter in _experienceMeters)
            foreach (var hero in _heroes)
                experienceMeter.ExperienceMeter.SetExperience(hero.Experience, _levelUpService.ExperienceForLevelUp);
        }
    }
}