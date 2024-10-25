using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    public class FinderAttackTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackLayerMaskView, Position, MoveDirection, RangeAttacker, Rotation, EntityTag>, Exc<Inactive, AttackEvent>> _filterArmy;
        
        private readonly EcsPoolInject<AttackEvent> _attackEventPool;
        
        public void Run(IEcsSystems systems)
        {
            var attackLayerMaskViewPool = _filterArmy.Pools.Inc1;
            var positionPool = _filterArmy.Pools.Inc2;
            var moveDirectionPool = _filterArmy.Pools.Inc3;
            var rangeAttackerPool = _filterArmy.Pools.Inc4;
            var rotationPool = _filterArmy.Pools.Inc5;

            foreach (var entity in _filterArmy.Value)
            {
                var layerMask = attackLayerMaskViewPool.Get(entity).Value;
                var position = positionPool.Get(entity).Value;
                var rangeAttack = rangeAttackerPool.Get(entity).Value;
                
                if (!IsAttackDistance(position, rangeAttack, layerMask, out var enemyId)) continue;
                
                Debug.Log($"[FinderAttackTargetSystem] Run {entity} attack {enemyId}");
                
                ref var moveDirection = ref moveDirectionPool.Get(entity);
                moveDirection.Value = Vector3.zero.normalized;

                var positionEnemy = positionPool.Get(enemyId);
                var directionToEnemy = (positionEnemy.Value - position).normalized;
                ref var rotationEntity = ref rotationPool.Get(entity);
                rotationEntity.Value = Quaternion.LookRotation(directionToEnemy);
                
                _attackEventPool.Value.Add(entity) = new AttackEvent();
            }
        }
        
        private bool IsAttackDistance(Vector3 currentPosition, float detectionRadius, int layerMask, out int enemyId)
        {
            var colliders = Physics.OverlapSphere(currentPosition, detectionRadius, layerMask);
            enemyId = -1;

            if (colliders.Length <= 0) return false;
            
            enemyId = colliders[0].GetComponent<Entity>().Id;

            return true;
        }
    }
}