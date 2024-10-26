using System;
using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.EcsEngine.Services
{
    public interface IEnemiesFactory
    {
        public TeamType GetTeamType();
        void InactiveObject(int id);
        public bool TryGetEnemy(EntityType entityType, Vector3 spawnPoint, Quaternion rotation, out Entity entity);
        public bool Spawn(out Entity entity);
        public bool FirstSpawn(EntityType entityType, int positionIndex, out Entity entity);
    }
    
    public class EnemiesFactory : IEnemiesFactory, IStartable, IDisposable
    {
        //How many enemies will be in the pool at startup.
        private const int DEFAULT_POOL_SIZE_ARMY = 5;
        //How many MAX enemies will be in the Active at game cycle.
        private const int MAX_ACTIVE_SIZE_ARMY = 2;
        
        private readonly TeamType _teamType;
        public TeamType GetTeamType() => _teamType;

        private readonly IEntityPool _entityPool;
        private readonly Vector3[] _spawnPoints;

        [Inject]
        public EnemiesFactory(TeamType teamType, EntityManager entityManager, Transform container, Transform worldTransform, bool autoExpand, Dictionary<EntityType, Entity> prefabs, Vector3[] spawnPoints, IObjectResolver resolver)
        {
            _teamType = teamType;
            _spawnPoints = spawnPoints;
            _entityPool = new EntityPool(entityManager, container, worldTransform, autoExpand, prefabs, resolver);
        }

        public void Start()
        {
            _entityPool.InstantiateDefault(DEFAULT_POOL_SIZE_ARMY);
        }

        public bool TryGetEnemy(EntityType entityType, Vector3 spawnPoint, Quaternion rotation, out Entity entity)
        {
            return _entityPool.TryGet(entityType, spawnPoint, rotation, out entity);
        }
        
        public bool Spawn(out Entity entity)
        {
            entity = default;
            
            if (_entityPool.GetActivesCount() >= MAX_ACTIVE_SIZE_ARMY)
                return false;

            if (!TryGetEnemy(EntityType.Archer, _spawnPoints[0], Quaternion.LookRotation(Vector3.forward), out var it))
                return false;

            entity = it;
            entity.GetData<Health>().Value = 5;

            return true;
        }
        
        public bool FirstSpawn(EntityType entityType, int positionIndex, out Entity entity)
        {
            return TryGetEnemy(entityType, _spawnPoints[positionIndex], Quaternion.LookRotation(Vector3.forward), out entity);
        }

        public async void InactiveObject(int id)
        {
            await UniTask.Delay(2000);
            _entityPool.InactiveObject(id);
        }

        public void Dispose()
        {
            _entityPool.ClearPool();
        }
    }
}