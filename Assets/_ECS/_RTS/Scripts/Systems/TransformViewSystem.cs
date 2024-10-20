using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS._RTS.Scripts.Systems
{
    public class TransformViewSystem : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position, Rotation, Attacking>> _filter;

        public void PostRun(IEcsSystems systems)
        {
            var transformViewPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var rotationPool = _filter.Pools.Inc3;
            var isAttackingPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                var isAttack = isAttackingPool.Get(entity).Value;
                ref var transform = ref transformViewPool.Get(entity);
                
                ref var rotation = ref rotationPool.Get(entity).Value;
                transform.Value.rotation = rotation;
                
                if (isAttack) continue;
                
                var position = positionPool.Get(entity);
                transform.Value.position = position.Value;
            }
        }
    }
}