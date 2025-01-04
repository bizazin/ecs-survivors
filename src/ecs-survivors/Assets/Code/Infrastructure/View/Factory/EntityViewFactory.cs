using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View.Factory
{
    public class EntityViewFactory : IEntityViewFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Vector3 _farAway = new(-999, 999, 0);
        private readonly IInstantiator _instantiator;

        public EntityViewFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public EntityBehaviour CreateViewForEntity(GameEntity entity)
        {
            var viewPrefab = _assetProvider.LoadAsset<EntityBehaviour>(entity.ViewPath);
            var view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
                viewPrefab,
                _farAway,
                Quaternion.identity,
                null);

            view.SetEntity(entity);

            return view;
        }

        public EntityBehaviour CreateViewForEntityFromPrefab(GameEntity entity)
        {
            var view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
                entity.ViewPrefab,
                _farAway,
                Quaternion.identity,
                null);

            view.SetEntity(entity);

            return view;
        }
    }
}