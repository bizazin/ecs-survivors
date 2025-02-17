using System;

namespace Code.Meta.UI.GoldHolder.Service
{
    public class StorageUIService : IStorageUIService
    {
        public event Action GoldChanged;
        public event Action GoldBoostChanged;

        public float CurrentGold { get; private set; }

        public float GoldGainBoost { get; private set; }

        public void UpdateGoldGainBoost(float boost)
        {
            GoldGainBoost = boost;
            GoldBoostChanged?.Invoke();
        }

        public void UpdateCurrentGold(float gold)
        {
            if (Math.Abs(gold - CurrentGold) > float.Epsilon)
            {
                CurrentGold = gold;
                GoldChanged?.Invoke();
            }
        }

        public void Cleanup()
        {
            CurrentGold = 0f;
            GoldGainBoost = 0f;

            GoldChanged = null;
            GoldBoostChanged = null;
        }
    }
}