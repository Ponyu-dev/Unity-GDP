using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems
{
    public class TransformViewSystem : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> _filter;
        private readonly EcsPoolInject<Rotation> _rotationPool;

        public void PostRun(IEcsSystems systems)
        {
            Debug.Log("[TransformViewSystem] Run");
            
            var rotationPool = _rotationPool.Value;

            foreach (var entity in _filter.Value)
            {
                ref var transform = ref _filter.Pools.Inc1.Get(entity);
                var position = _filter.Pools.Inc2.Get(entity);
                
                transform.Value.position = position.Value;

                if (!rotationPool.Has(entity)) continue;
                
                var rotation = rotationPool.Get(entity).Value;
                transform.Value.rotation = rotation;
            }
        }
    }
}