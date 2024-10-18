using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Sirenix.Utilities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems.Range
{
    public class AttackRangeSystem : IEcsRunSystem
    {
        protected readonly EcsFilterInject<Inc<Position, IsMoving, AttackRange, Layer>> _filter;

        public void Run(IEcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var isMovingPool = _filter.Pools.Inc2;
            var rangePool = _filter.Pools.Inc3;
            var layerPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                var position = positionPool.Get(entity).Value;
                var range = rangePool.Get(entity).Value;
                var layer = layerPool.Get(entity).Value;

                Debug.Log($"[AttackRangeSystem] {layer.ToString()} {LayerMask.NameToLayer(layer.ToString())}");
                var colliders = Physics.OverlapSphere(position, range, layer);
                
                ref var isMoving = ref isMovingPool.Get(entity);
                isMoving.Value = colliders.IsNullOrEmpty();
            }
        }
    }
}