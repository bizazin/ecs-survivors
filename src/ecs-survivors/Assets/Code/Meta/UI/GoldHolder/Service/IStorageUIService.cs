using System;

namespace Code.Meta.UI.GoldHolder.Service
{
    public interface IStorageUIService
    {
        float CurrentGold { get; }
        float GoldGainBoost { get; }
        event Action GoldChanged;
        void UpdateCurrentGold(float gold);
        void Cleanup();
        event Action GoldBoostChanged;
        void UpdateGoldGainBoost(float boost);
    }
}