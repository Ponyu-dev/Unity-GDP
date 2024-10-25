using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class TransformViewSynchronizerSystem : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position, Rotation>, Exc<Inactive>> _filter;

        public void PostRun(IEcsSystems systems)
        {
            var transformPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var rotationPool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                ref var transform = ref transformPool.Get(entity);
                var position = positionPool.Get(entity);
                
                transform.Value.position = position.Value;

                if (!rotationPool.Has(entity)) continue;
                
                var rotation = rotationPool.Get(entity).Value;
                transform.Value.rotation = rotation;
            }
        }
    }
}