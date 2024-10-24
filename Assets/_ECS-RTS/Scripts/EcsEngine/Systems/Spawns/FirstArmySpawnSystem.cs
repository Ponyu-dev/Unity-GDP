using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Spawns
{
    internal class FirstArmySpawnSystem : IEcsInitSystem
    {
        private readonly IReadOnlyList<IEnemiesFactory> _enemiesFactories;
        private bool _firstSpawn = false;

        [Inject]
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
                factory.FirstSpawn(EntityType.Swordsman, 0);
                factory.FirstSpawn(EntityType.Archer, 1);
            }
        }
    }
}