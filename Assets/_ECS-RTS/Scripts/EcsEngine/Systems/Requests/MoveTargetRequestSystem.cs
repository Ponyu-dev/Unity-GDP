using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class MoveTargetRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveTargetRequest, MoveTarget>, Exc<Inactive>> _filterRequest;
        private readonly EcsFilterInject<Inc<Position, Rotation, MoveDirection, EntityTag>, Exc<Inactive>> _filterPosition;
        
        private readonly EcsPoolInject<WalkEvent> _eventPool;
        
        public void Run(IEcsSystems systems)
        {
            var moveTargetRequestPool = _filterRequest.Pools.Inc1;
            var moveTargetPool = _filterRequest.Pools.Inc2;
            
            foreach (var id in _filterRequest.Value)
            {
                Debug.Log($"[MoveTargetRequestSystem] Run {id}");
                var positionPool = _filterPosition.Pools.Inc1;
                var rotationPool = _filterPosition.Pools.Inc2;
                var moveDirectionPool = _filterPosition.Pools.Inc3;
                
                moveTargetRequestPool.Del(id);
                var idMoveTarget = moveTargetPool.Get(id).Value;
                var enemyPosition = positionPool.Get(idMoveTarget).Value;

                var position = positionPool.Get(id);
                
                ref var moveDirection = ref moveDirectionPool.Get(id);
                var directionToEnemy = (enemyPosition - position.Value).normalized;
                moveDirection.Value = directionToEnemy;

                ref var rotation = ref rotationPool.Get(id);
                var targetRotation = Quaternion.LookRotation(directionToEnemy);
                rotation.Value = targetRotation;
                _eventPool.Value.Add(id) = new WalkEvent();
            }
        }
    }
}