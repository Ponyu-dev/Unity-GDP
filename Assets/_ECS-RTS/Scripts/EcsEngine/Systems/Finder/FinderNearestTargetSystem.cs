using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Finder
{
    public class FinderNearestTargetSystem : IEcsRunSystem
    {
        private const float RANGE_FINDER = 40;
        
        private readonly EcsFilterInject<Inc<FinderNearestTargetRequest>, Exc<Inactive>> _filter;
        private readonly EcsFilterInject<Inc<AttackLayerMaskView, Position, EntityTag>, Exc<Inactive>> _filterArmy;
        private readonly EcsFilterInject<Inc<MoveTarget, MoveTargetRequest>, Exc<Inactive>> _filterMove;
        
        private readonly EcsPoolInject<MoveTag> _poolMoveTag;

        public void Run(IEcsSystems systems)
        {
            var attackLayerMaskViewPool = _filterArmy.Pools.Inc1;
            var positionPool = _filterArmy.Pools.Inc2;
            
            var moveTargetPool = _filterMove.Pools.Inc1;
            var moveTargetRequestPool = _filterMove.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                Debug.Log($"[FinderNearestTargetSystem] Run {entity}");
                _filter.Pools.Inc1.Del(entity);

                var layerMask = attackLayerMaskViewPool.Get(entity);
                var position = positionPool.Get(entity);
                
                var nearestEnemy = FindNearestEnemy(position.Value, RANGE_FINDER, layerMask.Value);
                
                //Debug.Log($"[FinderAttackTargetSystem] Run nearestEnemy = {nearestEnemy} {enemyTeamPool.Get(nearestEnemy).Value}");
                if (nearestEnemy == -1) continue;

                _poolMoveTag.Value.Add(entity) = new MoveTag();
                moveTargetPool.Add(entity) = new MoveTarget { Value = nearestEnemy };
                moveTargetRequestPool.Add(entity) = new MoveTargetRequest();
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

                if (distance >= closestDistance) continue;
                
                closestDistance = distance;
                nearestEnemy = entityTarget.Id;
            }

            return nearestEnemy;
        }
    }
}