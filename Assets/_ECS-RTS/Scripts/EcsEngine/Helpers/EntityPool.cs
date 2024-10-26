using System;
using System.Collections.Generic;
using System.Linq;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.Helpers
{
    public interface IEntityPool
    {
        int GetActivesCount();
        void InstantiateDefault(int initialCount);
        bool TryGet(EntityType prefabKey, Vector3 position, Quaternion rotation, out Entity entity);
        void InactiveObject(Entity obj);
        void InactiveObject(int id);
        void DestroyEntity(int id);
        void ClearPool();
    }
    
    internal sealed class EntityPool : IEntityPool
    {
        private readonly EntityManager _entityManager;
        private readonly Transform _container;
        private readonly Transform _worldTransform;
        private readonly bool _autoExpand;
        private readonly IReadOnlyDictionary<EntityType, Entity> _prefabs;
        private readonly IObjectResolver _resolver;

        private Queue<Entity> _pool;
        private readonly HashSet<Entity> _actives;
        public int GetActivesCount() => _actives.Count;
        
        public EntityPool(
            EntityManager entityManager,
            Transform container,
            Transform worldTransform,
            bool autoExpand,
            IReadOnlyDictionary<EntityType, Entity> prefabs,
            IObjectResolver resolver)
        {
            _pool = new Queue<Entity>();
            _actives = new HashSet<Entity>();
            
            _entityManager = entityManager;
            _container = container;
            _worldTransform = worldTransform;
            _autoExpand = autoExpand;
            _prefabs = prefabs;
            _resolver = resolver;
        }

        public void InstantiateDefault(int initialCount)
        {
            for (var i = 0; i < initialCount; i++)
            {
                foreach (var prefabEntry in _prefabs)
                {
                    CreatePool(prefabEntry.Key);
                }
            }
        }
        
        private void CreatePool(EntityType prefabKey)
        {
            if (!TryGetPrefab(prefabKey, out var prefab)) return;

            var entity = CreateEntity(prefab, _container.position, Quaternion.identity, _container);
            _pool.Enqueue(entity);
            InactiveObject(entity);
        }
        
        public bool TryGet(EntityType prefabKey, Vector3 position, Quaternion rotation, out Entity entity)
        {
            entity = null;

            if (!TryDequeueOrCreate(prefabKey, position, rotation, out var obj)) 
                return false;

            ActivateEntity(obj, position, rotation);
            _actives.Add(obj);

            if (obj.HasData<Inactive>())
                obj.RemoveData<Inactive>();

            entity = obj;
            return true;
        }

        private bool TryDequeueOrCreate(EntityType prefabKey, Vector3 position, Quaternion rotation, out Entity entity)
        {
            // Попробуем взять из пула
            if (_pool.TryDequeue(out entity))
                return true;

            // Если авторасширение включено, пробуем создать новый объект
            if (_autoExpand && TryGetPrefab(prefabKey, out var prefab))
            {
                entity = CreateEntity(prefab, position, rotation, _worldTransform);
                return entity != null;
            }

            // Не удалось получить или создать объект
            entity = null;
            return false;
        }

        private bool TryGetPrefab(EntityType prefabKey, out Entity prefab)
        {
            if (_prefabs.TryGetValue(prefabKey, out prefab)) return true;
            
            Debug.LogError($"Prefab with key '{prefabKey}' not found.");
            return false;
        }

        private Entity CreateEntity(Entity prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var entity = _entityManager.Create(prefab, position, rotation, parent);

            //TODO ?? Maybe Form To Something ???
            var collisionView = entity.GetComponentInChildren<CollisionComponentView>(true);
            if (collisionView != null)
            {
                collisionView.Inject(_resolver.Resolve<ICollisionComponentPresenter>());
            }

            return entity;
        }

        private void ActivateEntity(Entity entity, Vector3 position, Quaternion rotation)
        {
            entity.transform.SetParent(_worldTransform);
            entity.GetData<Position>().Value = position;
            entity.GetData<Rotation>().Value = rotation;
        }

        public void InactiveObject(Entity obj)
        {
            if (obj == null || !_actives.Remove(obj)) return;
            
            if (!obj.HasData<Inactive>()) obj.AddData(new Inactive());
            
            obj.transform.SetParent(_container);
            _pool.Enqueue(obj);
        }

        public void InactiveObject(int id)
        {
            InactiveObject(_entityManager.Get(id));
        }

        public void DestroyEntity(int id)
        {
            try
            {
                if (_entityManager.Get(id) is not { } entity) return;

                if (_actives.Remove(entity))
                    _pool = new Queue<Entity>(_pool.Where(pooledObj => !pooledObj.Equals(entity)));

                _entityManager.Destroy(id);
            }
            catch (Exception exception)
            {
                Debug.LogError($"[EntityPool] Exception DestroyEntity {id}");
                Debug.LogException(exception);
            }
        }

        public void ClearPool()
        {
            _actives.Clear();
            _pool.Clear();
        }
    }
}