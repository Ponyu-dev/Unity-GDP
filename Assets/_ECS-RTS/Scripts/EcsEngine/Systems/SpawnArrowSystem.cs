using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class SpawnArrowSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ArrowRequest, SourceEntity, TargetEntity, Position>> _filter = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<FirePointView, EntityTag, TeamTag>, Exc<Inactive>> _filterEnemy;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        
        private readonly IArrowFactory _arrowFactory;
        
        public SpawnArrowSystem(IArrowFactory arrowFactory)
        {
            _arrowFactory = arrowFactory;
        }

        public void Run(IEcsSystems systems)
        {
            var sourceEntityPool = _filter.Pools.Inc2;
            var targetEntityPool = _filter.Pools.Inc3;
            var directionPositionPool = _filter.Pools.Inc4;
            
            var firePointViewPool = _filterEnemy.Pools.Inc1;
            var entityTagPool = _filterEnemy.Pools.Inc2;
            var teamTagPool = _filterEnemy.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                var sourceId = sourceEntityPool.Get(entity).Value;
                var targetId = targetEntityPool.Get(entity).Value;
                var entityType = entityTagPool.Get(sourceId).Value; 
                if (entityType != EntityType.Archer) continue;
                
                Debug.Log($"[SpawnArrowSystem] Run sourceId {sourceId} arrow to targetId {targetId} ");
                var directionToEnemy = directionPositionPool.Get(entity).Value;
                var teamType = teamTagPool.Get(sourceId);
                var firePoint = firePointViewPool.Get(sourceId).Value;
                _arrowFactory.FireArrow(teamType.Value, firePoint.position, directionToEnemy);
                
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}