using System;
using System.Collections.Generic;
using System.Linq;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Helpers.Pools;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = System.Random;

namespace _ECS_RTS.Scripts.EcsEngine.Services
{
    public interface IEnemiesFactory
    {
        public TeamType GetTeamType();
        public void InactiveObject(EntityType entityType, int id);
        public bool Spawn(EntityType entityType, int positionIndex, out Entity entity);
    }
    
    public class EnemiesFactory : IEnemiesFactory, IStartable, IDisposable
    {
        private static readonly EntityType[] _entityTypes = 
            Enum.GetValues(typeof(EntityType)).Cast<EntityType>().Where(e => e != EntityType.None).ToArray();

        //How many enemies will be in the pool at startup.
        private const int DEFAULT_POOL_SIZE_ARMY = 5;
        //How many MAX enemies will be in the Active at game cycle.
        private const int MAX_ACTIVE_SIZE_ARMY = 3;
        
        private readonly TeamType _teamType;
        public TeamType GetTeamType() => _teamType;
        
        private readonly Vector3[] _spawnPoints;
        
        private readonly Dictionary<EntityType, PoolEntity> _poolDictionary = new();

        [Inject]
        public EnemiesFactory(
            TeamType teamType,
            Transform container,
            Transform worldTransform,
            bool autoExpand,
            Dictionary<EntityType, Entity> army,
            Vector3[] spawnPoints,
            EntityManager entityManager,
            IObjectResolver resolver)
        {
            _teamType = teamType;
            _spawnPoints = spawnPoints;
            //_resolver = resolver;
            
            foreach (var prefab in army)
            {
                var pool = new PoolEntity(entityManager, resolver, prefab.Value, container, worldTransform, autoExpand);
                _poolDictionary.Add(prefab.Key, pool);
            }
        }

        public void Start()
        {
            for (var i = 0; i < DEFAULT_POOL_SIZE_ARMY; i++)
            {
                foreach (var pool in _poolDictionary.Values)
                {
                    pool.CreateObject(isEnqueue: true);
                }
            }
        }

        private bool IsMaxActiveSizeArmy()
        {
            var countAll = _poolDictionary.Values.Sum(pool => pool.ActivesCount());
            Debug.Log($"Spawn ActivesCount1 {countAll}");
            return countAll >= MAX_ACTIVE_SIZE_ARMY;
        }

        public bool Spawn(EntityType entityType, int positionIndex, out Entity entity)
        {
            entity = default;
            
            if (entityType == EntityType.None)
                entityType = _entityTypes[new Random().Next(0, _entityTypes.Length)];
            
            if (positionIndex == -1)
                positionIndex = new Random().Next(0, 2);
            
            if (!_poolDictionary.TryGetValue(entityType, out var pool)) return false;
            if (IsMaxActiveSizeArmy()) return false;
            if (!pool.TryGet(out var obj)) return false;
            
            pool.ActiveObject(obj.gameObject);
            entity = obj;
            
            ActivateEntity(entity, _spawnPoints[positionIndex], Quaternion.LookRotation(Vector3.forward));
            
            Debug.Log($"[EnemiesFactory] Spawn {_teamType.ToString()} {entityType.ToString()}");
            return true;
        }
        
        private void ActivateEntity(Entity entity, Vector3 position, Quaternion rotation)
        {
            entity.transform.position = position;
            
            entity.GetData<Health>().Value = 20;
            entity.GetData<Position>().Value = position;
            entity.GetData<Rotation>().Value = rotation;
            
            if (entity.HasData<Inactive>())
                entity.RemoveData<Inactive>();
        }

        public async void InactiveObject(EntityType entityType, int id)
        {
            await UniTask.Delay(2000);
            if (_poolDictionary.TryGetValue(entityType, out var pool))
            {
                pool.InactiveObject(id);
            }
        }

        public void Dispose()
        {
            foreach (var pool in _poolDictionary.Values)
            {
                pool.ClearPool();
            }
        }
    }
}