using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Random;
using Code.Gameplay.Features.Abilities.Factory;

namespace Code.Gameplay.Features.Abilities.Upgrade
{
    public class AbilityUpgradeService : IAbilityUpgradeService
    {
        private const int MinRepeatedAbilitiesToOffer = 1;
        private const int MaxCardsToOffer = 2;
        private readonly IAbilityFactory _abilityFactory;

        private readonly Dictionary<AbilityId, int> _currentAbilities;
        private readonly IRandomService _random;

        public AbilityUpgradeService(IRandomService randomService, IAbilityFactory abilityFactory)
        {
            _currentAbilities = new Dictionary<AbilityId, int>();
            _random = randomService;
            _abilityFactory = abilityFactory;
        }

        public int GetAbilityLevel(AbilityId abilityId)
        {
            return _currentAbilities.TryGetValue(abilityId, out var level)
                ? level
                : 0;
        }

        public void InitializeAbility(AbilityId ability)
        {
            if (!_currentAbilities.TryAdd(ability, 1))
                throw new Exception($"Ability {ability} is already initialized");

            switch (ability)
            {
                case AbilityId.VegetableBolt:
                    _abilityFactory.CreateVegetableBoltAbility(1);
                    break;
                case AbilityId.GarlicAura:
                    _abilityFactory.CreateGarlicAuraAbility();
                    break;
                case AbilityId.OrbitingMushroom:
                    _abilityFactory.CreateOrbitingMushroomAbility(1);
                    break;
                default:
                    throw new Exception($"Ability {ability} is not defined");
            }
        }

        public void UpgradeAbility(AbilityId ability)
        {
            if (_currentAbilities.ContainsKey(ability))
                _currentAbilities[ability]++;
            else
                InitializeAbility(ability);
        }

        public List<AbilityUpgradeOption> GetUpgradeOptions()
        {
            var repeatedAbilitiesToReturnCount = MinRepeatedAbilitiesToOffer +
                                                 _random.Range(0, Math.Min(_currentAbilities.Count, MaxCardsToOffer));
            var newAbilitiesToReturnCount = Math.Min(MaxCardsToOffer - repeatedAbilitiesToReturnCount,
                UnacquiredAbilities().Count);

            var upgradeOptions = GetRandomRepeatedAbilities(repeatedAbilitiesToReturnCount);
            upgradeOptions.AddRange(GetRandomUntappedAbilities(newAbilitiesToReturnCount));

            return upgradeOptions;
        }

        public void Cleanup()
        {
            _currentAbilities.Clear();
        }

        private List<AbilityUpgradeOption> GetRandomRepeatedAbilities(int count)
        {
            return _currentAbilities.Keys
                .OrderBy(_ => _random.Range(0, _currentAbilities.Count))
                .Take(count)
                .Select(abilityId => new AbilityUpgradeOption
                    { Id = abilityId, Level = _currentAbilities[abilityId] + 1 })
                .ToList();
        }

        private List<AbilityUpgradeOption> GetRandomUntappedAbilities(int count)
        {
            return UnacquiredAbilities()
                .OrderBy(_ => _random.Range(0, UnacquiredAbilities().Count))
                .Take(count)
                .Select(abilityId => new AbilityUpgradeOption { Id = abilityId, Level = 1 })
                .ToList();
        }

        private List<AbilityId> UnacquiredAbilities()
        {
            return Enum
                .GetValues(typeof(AbilityId))
                .Cast<AbilityId>()
                .Except(_currentAbilities.Keys)
                .Except(new[] { AbilityId.Unknown })
                .ToList();
        }
    }
}