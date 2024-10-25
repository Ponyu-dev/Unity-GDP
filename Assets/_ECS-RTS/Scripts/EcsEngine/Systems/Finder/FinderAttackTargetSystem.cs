using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Finder
{
    public class FinderAttackTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackLayerMaskView, Position, RangeAttacker, EntityTag, MoveTag>, Exc<Inactive>> _filterArmy;
        private readonly EcsFilterInject<Inc<AttackTargetRequest, AttackTargetEntity>, Exc<Inactive>> _filterAttack;
        
        public void Run(IEcsSystems systems)
        {
            var attackLayerMaskViewPool = _filterArmy.Pools.Inc1;
            var positionPool = _filterArmy.Pools.Inc2;
            var rangeAttackerPool = _filterArmy.Pools.Inc3;

            foreach (var entity in _filterArmy.Value)
            {
                var layerMask = attackLayerMaskViewPool.Get(entity).Value;
                var position = positionPool.Get(entity).Value;
                var rangeAttack = rangeAttackerPool.Get(entity).Value;
                
                if (!IsAttackDistance(position, rangeAttack, layerMask, out var enemyId)) continue;
                
                Debug.Log($"[FinderAttackTargetSystem] Run {entity} attack {enemyId}");

                _filterAttack.Pools.Inc1.Add(entity) = new AttackTargetRequest();
                _filterAttack.Pools.Inc2.Add(entity) = new AttackTargetEntity { Value = enemyId};
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