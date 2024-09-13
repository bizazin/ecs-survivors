using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<AbilityId, AbilityConfig> _abilityId;

        public void LoadAll()
        {
            LoadAbilities();
        }

        public AbilityConfig GetAbilityById(AbilityId abilityId)
        {
            if (_abilityId.TryGetValue(abilityId, out AbilityConfig config))
                return config;

            throw new Exception($"Ability config for ability {abilityId} was not found");
        }

        public AbilityLevel GetAbilityLevel(AbilityId abilityId, int level)
        {
            var config = GetAbilityById(abilityId);

            if (level > config.Levels.Count)
                level = config.Levels.Count;


            return config.Levels[level - 1];
        }

        private void LoadAbilities()
        {
            _abilityId = Resources.LoadAll<AbilityConfig>("Configs/Abilities")
                .ToDictionary(x => x.AbilityId, x => x);
        }
    }
}