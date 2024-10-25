using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class MoveTargetRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, Rotation, MoveDirection, MoveTarget, MoveTag, EntityTag>, Exc<Inactive>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var rotationPool = _filter.Pools.Inc2;
            var moveDirectionPool = _filter.Pools.Inc3;
            var moveTargetPool = _filter.Pools.Inc4;
            
            foreach (var id in _filter.Value)
            {
                var idMoveTarget = moveTargetPool.Get(id).Value;
                var enemyPosition = positionPool.Get(idMoveTarget).Value;

                var position = positionPool.Get(id);
                
                ref var moveDirection = ref moveDirectionPool.Get(id);
                var directionToEnemy = (enemyPosition - position.Value).normalized;
                moveDirection.Value = directionToEnemy;

                ref var rotation = ref rotationPool.Get(id);
                var targetRotation = Quaternion.LookRotation(directionToEnemy);
                rotation.Value = targetRotation;
            }
        }
    }
}