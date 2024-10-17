using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, MoveDirection, MoveSpeed>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            Debug.Log("[MovementSystem] Run");
            var deltaTime = Time.deltaTime;
            
            var positionPool = _filter.Pools.Inc1;
            var directionPool = _filter.Pools.Inc2;
            var speedPool = _filter.Pools.Inc3;
            
            foreach (int entity in _filter.Value)
            {
                var moveDirection = directionPool.Get(entity);
                var moveSpeed = speedPool.Get(entity);
                ref var position = ref positionPool.Get(entity);
                position.Value += moveDirection.Value * (moveSpeed.Value * deltaTime);
            }
        }
    }
}