using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    public class NearestTargetFinderRequestSystem : IEcsRunSystem
    {
        private const float RANGE_FINDER = 40;
        
        private readonly EcsFilterInject<Inc<FirstTargetSelectedRequest>, Exc<Inactive>> _filter;
        
        private readonly EcsFilterInject<Inc<AttackLayerMaskView, Position, Rotation, MoveDirection, TeamTag, EntityTag>, Exc<Inactive>> _filterArmy;
        private readonly EcsFilterInject<Inc<Position, TeamTag>, Exc<Inactive>> _filterEnemyArmy;
        
        private readonly EcsPoolInject<WalkEvent> _eventPool;
        
        public void Run(IEcsSystems systems)
        {
            var attackLayerMaskViewPool = _filterArmy.Pools.Inc1;
            var positionPool = _filterArmy.Pools.Inc2;
            var rotationPool = _filterArmy.Pools.Inc3;
            var moveDirectionPool = _filterArmy.Pools.Inc4;

            var enemyPositionPool = _filterEnemyArmy.Pools.Inc1;
            var enemyTeamPool = _filterEnemyArmy.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);

                var layerMask = attackLayerMaskViewPool.Get(entity);
                var position = positionPool.Get(entity);
                ref var rotation = ref rotationPool.Get(entity);
                ref var moveDirection = ref moveDirectionPool.Get(entity);
                
                var nearestEnemy = FindNearestEnemy(position.Value, RANGE_FINDER, layerMask.Value);
                
                //Debug.Log($"[FinderAttackTargetSystem] Run nearestEnemy = {nearestEnemy} {enemyTeamPool.Get(nearestEnemy).Value}");
                if (nearestEnemy == -1) continue;
                
                var enemy = enemyPositionPool.Get(nearestEnemy);
                var enemyPosition = enemy.Value;
                    
                var directionToEnemy = (enemyPosition - position.Value).normalized;
                moveDirection.Value = directionToEnemy;
                    
                var targetRotation = Quaternion.LookRotation(directionToEnemy);
                rotation.Value = targetRotation;
                    
                _eventPool.Value.Add(entity) = new WalkEvent();
            }
        }
        
        private int FindNearestEnemy(Vector3 currentPosition, float detectionRadius, int layerMask)
        {
            var closestDistance = float.MaxValue;
            var nearestEnemy = -1;
            var hitColliders = Physics.OverlapSphere(currentPosition, detectionRadius, layerMask);

            foreach (var collider in hitColliders)
            {
                if (!collider.TryGetComponent<Entity>(out var entityTarget)) 
                    continue;

                var distance = Vector3.Distance(currentPosition, collider.transform.position);

                if (!(distance < closestDistance)) continue;
                
                closestDistance = distance;
                nearestEnemy = entityTarget.Id;
            }

            return nearestEnemy;
        }
    }
}