using System;
using System.Collections.Generic;
using System.Linq;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.Helpers.Pools
{
    public sealed class PoolEntity : PoolObject<Entity>
    {
        private readonly EntityManager _entityManager;
        private readonly IObjectResolver _objectResolver;
        
        public PoolEntity(
            EntityManager entityManager, IObjectResolver objectResolver, Entity prefab, Transform container, Transform worldTransform, bool autoExpand)
            : base(prefab, container, worldTransform, autoExpand)
        {
            _entityManager = entityManager;
            _objectResolver = objectResolver;
        }

        public override Entity CreateObject(Transform container = null, bool isEnqueue = false)
        {
            var entity = _entityManager.Create(Prefab, Vector3.zero, Quaternion.identity, container == null ? Container : container);
            var collisionView = entity.GetComponentInChildren<CollisionComponentView>(true);
            if (collisionView != null)
                _objectResolver.Inject(collisionView);

            InactiveObject(entity.Id);
            if (isEnqueue) Pool.Enqueue(entity);
            return entity;
        }

        public void ActiveObject(GameObject obj)
        {
            obj.transform.SetParent(WorldTransform);
        }

        public void InactiveObject(int id)
        {
            var entity = _entityManager.Get(id);
            if (!entity.HasData<Inactive>()) entity.AddData(new Inactive());
            entity.transform.SetParent(Container);
            
            if (!Actives.Remove(entity)) return;
            Pool.Enqueue(entity);
        }
        
        public void DestroyEntity(int id)
        {
            try
            {
                if (_entityManager.Get(id) is not { } entity) return;

                if (Actives.Remove(entity))
                    Pool = new Queue<Entity>(Pool.Where(pooledObj => !pooledObj.Equals(entity)));

                _entityManager.Destroy(id);
            }
            catch (Exception exception)
            {
                Debug.LogError($"[EntityPool] Exception DestroyEntity {id}");
                Debug.LogException(exception);
            }
        }

        public override void ClearPool()
        {
            Pool.Clear();
            Actives.Clear();
        }
    }
}