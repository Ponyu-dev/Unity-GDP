using System;
using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.EcsEngine.Services
{
    public interface IEnemiesFactory
    {
        public TeamType GetTeamType(); 
        public bool TryGetEnemy(EntityType entityType, Vector3 spawnPoint, Quaternion rotation, out Entity entity);
    }
    
    public class EnemiesFactory : IEnemiesFactory, IStartable, ITickable, IDisposable
    {
        //How many enemies will be in the pool at startup.
        private const int DEFAULT_POOL_SIZE_ARMY = 5;
        
        private readonly TeamType _teamType;
        public TeamType GetTeamType() => _teamType;
        
        private readonly IEntityPool _entityPool;
        private readonly Vector3[] _spawnPoints;

        [Inject]
        public EnemiesFactory(TeamType teamType, EntityManager entityManager, Transform container, Transform worldTransform, bool autoExpand, Dictionary<EntityType, Entity> prefabs, Vector3[] spawnPoints)
        {
            _teamType = teamType;
            _spawnPoints = spawnPoints;
            _entityPool = new EntityPool(entityManager, container, worldTransform, autoExpand, prefabs);
        }

        public void Start()
        {
            _entityPool.InstantiateDefault(DEFAULT_POOL_SIZE_ARMY);
        }

        public bool TryGetEnemy(EntityType entityType, Vector3 spawnPoint, Quaternion rotation, out Entity entity)
        {
            if (_entityPool.TryGet(entityType, spawnPoint, rotation, out var ent))
            {
                entity = ent;
                return true;
            }
            
            entity = default;
            return false;
        }

        public void Tick()
        {
            FirstSpawn();
        }

        private bool _firstSpawn = false;
        private void FirstSpawn()
        {
            if (_firstSpawn) return;
            
            _firstSpawn = true;
            TryGetEnemy(EntityType.Swordsman, _spawnPoints[0], Quaternion.LookRotation(Vector3.forward), out var sword);
            TryGetEnemy(EntityType.Archer, _spawnPoints[1], Quaternion.LookRotation(Vector3.forward), out var archer);
        }

        public void Dispose()
        {
            _entityPool.ClearPool();
        }
    }
}