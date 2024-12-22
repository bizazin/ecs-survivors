using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.LevelUp
{
    [CreateAssetMenu(menuName = "ECS Survivors/Level up Config", fileName = "levelUpConfig")]
    public class LevelUpConfig : ScriptableObject
    {
        public int MaxLevel;
        public List<float> ExperienceForLevel;
    }
}