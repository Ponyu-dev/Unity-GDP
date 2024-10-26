using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Spawns
{
    internal sealed class DelayArmySpawnSystem : TimedSystem
    {
        private readonly EcsPoolInject<FinderNearestTargetRequest> _firstTargetSelectedPool;
        private readonly IReadOnlyList<IEnemiesFactory> _enemiesFactories;
        
        private const float SpawnDelay = 4f;
        
        public DelayArmySpawnSystem(IReadOnlyList<IEnemiesFactory> enemiesFactories)
            : base(SpawnDelay)
        {
            _enemiesFactories = enemiesFactories;
        }
        
        protected override void Execute(IEcsSystems systems)
        {
            foreach (var factory in _enemiesFactories)
            {
                if (factory.Spawn(out var entity))
                    _firstTargetSelectedPool.Value.Add(entity.Id) = new FinderNearestTargetRequest();
            }
        }
    }
}