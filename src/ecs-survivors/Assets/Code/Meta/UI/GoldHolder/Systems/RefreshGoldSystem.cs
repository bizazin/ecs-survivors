using Code.Meta.UI.GoldHolder.Service;
using Entitas;

namespace Code.Meta.UI.GoldHolder.Systems
{
    public class RefreshGoldSystem : IExecuteSystem
    {
        private readonly IStorageUIService _storage;
        private readonly IGroup<MetaEntity> _storages;

        public RefreshGoldSystem(MetaContext meta, IStorageUIService storage)
        {
            _storage = storage;
            _storages = meta.GetGroup(MetaMatcher
                .AllOf(
                    MetaMatcher.Storage,
                    MetaMatcher.Gold));
        }

        public void Execute()
        {
            foreach (var storage in _storages)
                _storage.UpdateCurrentGold(storage.Gold);
        }
    }
}