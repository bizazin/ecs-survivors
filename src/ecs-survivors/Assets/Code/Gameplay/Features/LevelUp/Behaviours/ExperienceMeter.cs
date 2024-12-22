using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.LevelUp.Behaviours
{
    public class ExperienceMeter : MonoBehaviour
    {
        public Slider ProgressBar;

        public void SetExperience(float heroExperience, float experienceForLevelUp) => 
            ProgressBar.value = heroExperience / experienceForLevelUp;
    }
}