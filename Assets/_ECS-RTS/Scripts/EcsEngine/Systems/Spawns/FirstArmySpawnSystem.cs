using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Spawns
{
    internal class FirstArmySpawnSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<FinderNearestTargetRequest> _firstTargetSelectedPool;
        
        private readonly IReadOnlyList<IEnemiesFactory> _enemiesFactories;
        private bool _firstSpawn;
        
        public FirstArmySpawnSystem(IReadOnlyList<IEnemiesFactory> enemiesFactories)
        {
            _enemiesFactories = enemiesFactories;
        }

        public void Init(IEcsSystems systems)
        {
            if (_firstSpawn) return;
            
            _firstSpawn = true;
            foreach (var factory in _enemiesFactories)
            {
                if (factory.FirstSpawn(EntityType.Swordsman, 0, out var entitySwordsman))
                    _firstTargetSelectedPool.Value.Add(entitySwordsman.Id) = new FinderNearestTargetRequest();

                if (factory.FirstSpawn(EntityType.Archer, 1, out var entityArcher))
                    _firstTargetSelectedPool.Value.Add(entityArcher.Id) = new FinderNearestTargetRequest();
            }
        }
    }
}