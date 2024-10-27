using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class AttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackTargetRequest, AttackTargetEntity>, Exc<Inactive>> _filterAttackRequest;
        private readonly EcsFilterInject<Inc<MoveTag, MoveTarget, MoveDirection, Position, Rotation>, Exc<Inactive>> _filterMove;
        
        private readonly EcsPoolInject<AttackTag> _poolAttackTag;
        private readonly EcsPoolInject<EntityTag> _entityPool;

        private readonly EcsFilterInject<Inc<SourceEntity, AttackTargetEntity, Position, AttackEvent>> _filterEvent = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _ecsWorldEvent = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            var moveTagPool = _filterMove.Pools.Inc1;
            var moveTargetPool = _filterMove.Pools.Inc2;
            var moveDirectionPool = _filterMove.Pools.Inc3;
            var positionPool = _filterMove.Pools.Inc4;
            var rotationPool = _filterMove.Pools.Inc5;

            var attackTargetRequestPool = _filterAttackRequest.Pools.Inc1;
            var attackTargetEntityPool = _filterAttackRequest.Pools.Inc2;

            var eventSourceEntityPool = _filterEvent.Pools.Inc1;
            var eventAttackTargetEntityPool = _filterEvent.Pools.Inc2;
            var eventPositionPool = _filterEvent.Pools.Inc3;
            var eventAttackEventPool = _filterEvent.Pools.Inc4;
            
            foreach (var entity in _filterAttackRequest.Value)
            {
                Debug.Log($"[AttackRequestSystem] Run {entity}");
                
                if (moveTagPool.Has(entity))
                    moveTagPool.Del(entity);
                if (moveTargetPool.Has(entity))
                    moveTargetPool.Del(entity);
                
                ref var moveDirection = ref moveDirectionPool.Get(entity);
                moveDirection.Value = Vector3.zero.normalized;

                var position = positionPool.Get(entity).Value;
                var idTarget = attackTargetEntityPool.Get(entity).Value;
                var positionEnemy = positionPool.Get(idTarget).Value;
                
                var directionToEnemy = (positionEnemy - position).normalized;
                
                ref var rotationEntity = ref rotationPool.Get(entity);
                rotationEntity.Value = Quaternion.LookRotation(directionToEnemy);
                
                if (_poolAttackTag.Value.Has(entity))
                    _poolAttackTag.Value.Add(entity) = new AttackTag();

                var eventId = _ecsWorldEvent.Value.NewEntity();
                eventSourceEntityPool.Add(eventId) = new SourceEntity { Value = entity };
                eventAttackTargetEntityPool.Add(eventId) = new AttackTargetEntity { Value = idTarget };
                eventPositionPool.Add(eventId) = new Position { Value = directionToEnemy };
                eventAttackEventPool.Add(eventId) = new AttackEvent();
                
                attackTargetRequestPool.Del(entity);
                attackTargetEntityPool.Del(entity);
            }
        }

        
    }
}