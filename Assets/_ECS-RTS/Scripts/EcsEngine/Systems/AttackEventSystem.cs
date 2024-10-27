using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class AttackEventSystem : IEcsRunSystem
    {
        private static readonly int AAttackAnimatorTrigger = Animator.StringToHash("AttackA");
        private static readonly int BAttackAnimatorTrigger = Animator.StringToHash("AttackB");
        
        private readonly EcsFilterInject<Inc<SourceEntity, AttackTargetEntity, Position, AttackEvent>> _filterEvent = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _ecsWorldEvent = EcsWorlds.EVENTS;
        
        private readonly EcsFilterInject<Inc<AnimatorView, AnimatorTrigger, AnimEvent>> _filterAnimEvent = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<EntityTag, AnimatorView, DelayAttack, LastRunTimeAttack>, Exc<Inactive>> _filterEnemy;

        public void Run(IEcsSystems systems)
        {
            Debug.Log("[AttackEventSystem] Execute");

            var deltaTime = Time.deltaTime;

            var eventSourceEntityPool = _filterEvent.Pools.Inc1;
            var eventAttackTargetEntityPool = _filterEvent.Pools.Inc2;
            var eventPositionPool = _filterEvent.Pools.Inc3;

            var entityTagPool = _filterEnemy.Pools.Inc1;
            var animatorViewPool = _filterEnemy.Pools.Inc2;
            var delayAttackPool = _filterEnemy.Pools.Inc3;
            var lastRunTimeAttackPool = _filterEnemy.Pools.Inc4;

            var eventAnimatorViewPool = _filterAnimEvent.Pools.Inc1;
            var eventAnimatorTriggerPool = _filterAnimEvent.Pools.Inc2;
            var eventAnimPool = _filterAnimEvent.Pools.Inc3;

            foreach (var @event in _filterEvent.Value)
            {
                var sourceEntity = eventSourceEntityPool.Get(@event).Value;

                var delayAttack = delayAttackPool.Get(sourceEntity).Value;
                ref var lastRunTimeAttack = ref lastRunTimeAttackPool.Get(sourceEntity).Value;
                lastRunTimeAttack += deltaTime;

                if (lastRunTimeAttack < delayAttack)
                {
                    _ecsWorldEvent.Value.DelEntity(@event);
                    continue;
                }

                lastRunTimeAttack = 0;
                var targetEntity = eventAttackTargetEntityPool.Get(@event).Value;
                var directionToEnemy = eventPositionPool.Get(@event).Value;

                Debug.Log($"[AttackEventSystem] Run {sourceEntity} attack {targetEntity}");

                var randomIndex = Random.Range(0, 2);
                var trigger = randomIndex == 0 ? AAttackAnimatorTrigger : BAttackAnimatorTrigger;
                var animEvent = _ecsWorldEvent.Value.NewEntity();
                var animView = animatorViewPool.Get(sourceEntity).Value;
                eventAnimatorViewPool.Add(animEvent) = new AnimatorView { Value = animView };
                eventAnimatorTriggerPool.Add(animEvent) = new AnimatorTrigger { Value = trigger };
                eventAnimPool.Add(animEvent) = new AnimEvent();

                if (entityTagPool.Get(sourceEntity).Value == EntityType.Archer)
                    AddArrowEvent(sourceEntity, targetEntity, directionToEnemy);

                _ecsWorldEvent.Value.DelEntity(@event);
            }
        }

        private readonly EcsPoolInject<ArrowRequest> _arrowRequestPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SourceEntity> _sourceEntityPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        
        private void AddArrowEvent(int idSource, int idTarget, Vector3 directionToEnemy)
        {
            var arrow = _ecsWorldEvent.Value.NewEntity();
            _arrowRequestPool.Value.Add(arrow) = new ArrowRequest();
            _sourceEntityPool.Value.Add(arrow) = new SourceEntity { Value = idSource };
            _targetEntityPool.Value.Add(arrow) = new TargetEntity { Value = idTarget };
            _positionPool.Value.Add(arrow) = new Position { Value = directionToEnemy };
        }
    }
}