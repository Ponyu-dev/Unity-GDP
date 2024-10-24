using System;
using System.Collections.Generic;
using System.Linq;
using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Helpers
{
    public interface IEntityPool
    {
        void InstantiateDefault(int initialCount);
        bool TryGet(EntityType prefabKey, Vector3 position, Quaternion rotation, out Entity entity);
        void InactiveObject(Entity obj);
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

        private Queue<Entity> _pool;
        private readonly HashSet<Entity> _actives;
        
        public EntityPool(
            EntityManager entityManager,
            Transform container,
            Transform worldTransform,
            bool autoExpand,
            IReadOnlyDictionary<EntityType, Entity> prefabs)
        {
            _pool = new Queue<Entity>();
            _actives = new HashSet<Entity>();
            
            _entityManager = entityManager;
            _container = container;
            _worldTransform = worldTransform;
            _autoExpand = autoExpand;
            _prefabs = prefabs;
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
            if (!_prefabs.TryGetValue(prefabKey, out var prefab))
            {
                Debug.LogError($"Prefab with key '{prefabKey}' not found.");
                return;
            }
            
            var entity = _entityManager.Create(prefab, _container.position, Quaternion.identity, _container);
            _pool.Enqueue(entity);
            InactiveObject(entity);
        }

        public bool TryGet(EntityType prefabKey, Vector3 position, Quaternion rotation, out Entity entity)
        {
            entity = null;
            
            if (_pool.TryDequeue(out var obj))
            {
                obj.transform.SetParent(_worldTransform);
                obj.GetData<Position>().Value = position;
                obj.GetData<Rotation>().Value = rotation;
                //obj.Initialize(this.world);
            }
            else if (_autoExpand)
            {
                if (!_prefabs.TryGetValue(prefabKey, out var prefab))
                {
                    Debug.LogError($"Prefab with key '{prefabKey}' not found.");
                    return false;
                }

                obj = _entityManager.Create(prefab, position, rotation, _worldTransform);
            }

            if (obj == null) return false;
            
            _actives.Add(obj);
            if (obj.HasData<Inactive>())
                obj.RemoveData<Inactive>();
            
            entity = obj;
            
            return true;
        }

        public void InactiveObject(Entity obj)
        {
            if (obj == null || !_actives.Remove(obj)) return;
            
            obj.AddData(new Inactive());
            obj.transform.SetParent(_container);
            _pool.Enqueue(obj);
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