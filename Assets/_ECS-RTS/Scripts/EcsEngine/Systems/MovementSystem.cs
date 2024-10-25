using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position, EntityTag>, Exc<Inactive>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            var directionPool = _filter.Pools.Inc1;
            var speedPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                var moveDirection = directionPool.Get(entity);
                var moveSpeed = speedPool.Get(entity);
                ref var position = ref positionPool.Get(entity);
                position.Value += moveDirection.Value * (moveSpeed.Value * deltaTime);
            }
        }
    }
}