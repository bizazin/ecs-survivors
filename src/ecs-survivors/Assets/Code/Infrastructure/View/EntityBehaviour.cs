using Code.Gameplay.Common.Collisions;
using Code.Infrastructure.View.Registrars;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View
{
    public class EntityBehaviour : MonoBehaviour, IEntityView
    {
        private ICollisionRegistry _collisionRegistry;
        public GameEntity Entity { get; private set; }

        public void SetEntity(GameEntity entity)
        {
            Entity = entity;
            Entity.AddView(this);
            Entity.Retain(this);

            foreach (var registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
                registrar.RegisterComponents();

            foreach (var collider2d in GetComponentsInChildren<Collider2D>(true))
                _collisionRegistry.Register(collider2d.GetInstanceID(), Entity);
        }

        public void ReleaseEntity()
        {
            foreach (var registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
                registrar.UnregisterComponents();

            foreach (var collider2d in GetComponentsInChildren<Collider2D>(true))
                _collisionRegistry.Unregister(collider2d.GetInstanceID());

            Entity.Release(this);
            Entity = null;
        }

        [Inject]
        private void Construct(ICollisionRegistry collisionRegistry)
        {
            _collisionRegistry = collisionRegistry;
        }
    }
}