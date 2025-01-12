using UnityEngine;

namespace Code.Meta.Features.AfkGain.Configs
{
    [CreateAssetMenu(menuName = "ECS Survivors/AfkGainConfig", fileName = "AfkGainConfig")]
    public class AfkGainConfig : ScriptableObject
    {
        public float GoldPerSecond = 1;
    }
}