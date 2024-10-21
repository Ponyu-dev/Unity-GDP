using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Sirenix.Utilities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems.Range
{
    public class AttackRangeSystem : IEcsRunSystem
    {
        protected readonly EcsFilterInject<Inc<Position, IsMoving, Attacking, AttackRange, Layer>> _filter;

        public void Run(IEcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var isMovingPool = _filter.Pools.Inc2;
            var isAttackingPool = _filter.Pools.Inc3;
            var rangePool = _filter.Pools.Inc4;
            var layerPool = _filter.Pools.Inc5;

            foreach (var entity in _filter.Value)
            {
                var position = positionPool.Get(entity).Value;
                var range = rangePool.Get(entity).Value;
                var layer = layerPool.Get(entity).Value;
                
                var colliders = Physics.OverlapSphere(position, range, layer);
                
                ref var isMoving = ref isMovingPool.Get(entity);
                ref var isAttacking = ref isAttackingPool.Get(entity);
                
                isMoving.Value = colliders.IsNullOrEmpty();
                isAttacking.Value = !isMoving.Value;
                isAttacking.Target = isAttacking.Value ? colliders[0].transform : default;
                
                Debug.Log($"[AttackRangeSystem] attack {entity} isMoving.Value = {isMoving.Value} isAttacking.Value = {isAttacking.Value}");
            }
        }
    }
}