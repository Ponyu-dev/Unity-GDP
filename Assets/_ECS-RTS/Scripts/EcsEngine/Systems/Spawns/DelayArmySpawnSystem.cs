using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Spawns
{
    internal sealed class DelayArmySpawnSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<FinderNearestTargetRequest> _firstTargetSelectedPool;
        private readonly IReadOnlyList<IEnemiesFactory> _enemiesFactories;

        [Inject]
        public DelayArmySpawnSystem(IReadOnlyList<IEnemiesFactory> enemiesFactories)
        {
            _enemiesFactories = enemiesFactories;
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var factory in _enemiesFactories)
            {
                if (factory.DelaySpawn(out var entity))
                {
                    _firstTargetSelectedPool.Value.Add(entity.Id) = new FinderNearestTargetRequest();
                }
            }
        }
    }
}