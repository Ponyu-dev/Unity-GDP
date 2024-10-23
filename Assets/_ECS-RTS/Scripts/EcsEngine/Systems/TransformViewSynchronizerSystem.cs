using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class TransformViewSynchronizerSystem : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> _filter;
        private readonly EcsPoolInject<Rotation> _rotationPool;

        public void PostRun(IEcsSystems systems)
        {
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