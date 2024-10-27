using System.Collections.Generic;
using System.Linq;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class EnemyDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TeamTag, EntityTag, Inactive, DeathEvent>> _filter;
        private readonly IReadOnlyList<IEnemiesFactory> _enemiesFactories;
        
        public EnemyDestroySystem(IReadOnlyList<IEnemiesFactory> enemiesFactories)
        {
            _enemiesFactories = enemiesFactories;
        }

        public void Run(IEcsSystems systems)
        {
            var teamTagPool = _filter.Pools.Inc1;
            var entityTagPool = _filter.Pools.Inc2;
            
            foreach (var @event in _filter.Value)
            {
                var team = teamTagPool.Get(@event).Value;
                var entityType = entityTagPool.Get(@event).Value;
                Debug.Log($"[EnemyDestroySystem] {@event} DeathEvent");
                _enemiesFactories.First(it => it.GetTeamType() == team).InactiveObject(entityType, @event);
            }
        }
    }
}