using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
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
        private readonly EcsPoolInject<IdleEvent> _eventPool;
        private readonly EcsPoolInject<EntityTag> _entityPool;
        
        public void Run(IEcsSystems systems)
        {
            var moveTagPool = _filterMove.Pools.Inc1;
            var moveTargetPool = _filterMove.Pools.Inc2;
            var moveDirectionPool = _filterMove.Pools.Inc3;
            var positionPool = _filterMove.Pools.Inc4;
            var rotationPool = _filterMove.Pools.Inc5;

            var attackTargetRequestPool = _filterAttackRequest.Pools.Inc1;
            var attackTargetEntityPool = _filterAttackRequest.Pools.Inc2;
            
            foreach (var entity in _filterAttackRequest.Value)
            {
                attackTargetRequestPool.Del(entity);
                
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

                if (_entityPool.Value.Get(entity).Value == EntityType.Archer)
                    AddArrowEvent(entity, idTarget, directionToEnemy);
                
                //_eventPool.Value.Add(entity) = new IdleEvent();
                _poolAttackTag.Value.Add(entity) = new AttackTag();
            }
        }

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<ArrowRequest> _arrowRequestPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SourceEntity> _sourceEntityPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        
        private void AddArrowEvent(int idSource, int idTarget, Vector3 directionToEnemy)
        {
            var arrow = _eventWorld.Value.NewEntity();
            _arrowRequestPool.Value.Add(arrow) = new ArrowRequest();
            _sourceEntityPool.Value.Add(arrow) = new SourceEntity { Value = idSource };
            _targetEntityPool.Value.Add(arrow) = new TargetEntity { Value = idTarget };
            _positionPool.Value.Add(arrow) = new Position { Value = directionToEnemy };
        }
    }
}