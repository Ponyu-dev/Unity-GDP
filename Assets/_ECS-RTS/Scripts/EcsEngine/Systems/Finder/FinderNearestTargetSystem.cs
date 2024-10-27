using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Finder
{
    public class FinderNearestTargetSystem : IEcsRunSystem
    {
        private const float RANGE_FINDER = 50;
        
        private readonly EcsFilterInject<Inc<FinderNearestTargetRequest>, Exc<Inactive>> _filter;
        private readonly EcsFilterInject<Inc<AttackLayerMaskView, Position, EntityTag>, Exc<Inactive>> _filterArmy;
        private readonly EcsFilterInject<Inc<MoveTarget, MoveTag>, Exc<Inactive>> _filterMove;

        private static readonly int WalkAnimatorTrigger = Animator.StringToHash("Walk");
        private readonly EcsPoolInject<AnimatorView> _animatorViewPool;
        private readonly EcsFilterInject<Inc<AnimatorView, AnimatorTrigger, AnimEvent>> _filterEvent = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _ecsWorldEvent = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            var attackLayerMaskViewPool = _filterArmy.Pools.Inc1;
            var positionPool = _filterArmy.Pools.Inc2;
            
            var moveTargetPool = _filterMove.Pools.Inc1;
            var moveTagPool = _filterMove.Pools.Inc2;
            
            var animatorViewPool = _filterEvent.Pools.Inc1;
            var animatorTriggerPool = _filterEvent.Pools.Inc2;
            var animEventPool = _filterEvent.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);

                var layerMask = attackLayerMaskViewPool.Get(entity);
                var position = positionPool.Get(entity);
                
                if (!FindNearestEnemy(position.Value, RANGE_FINDER, layerMask.Value, out var nearestEnemy))
                    continue;
                
                moveTargetPool.Add(entity) = new MoveTarget { Value = nearestEnemy };
                moveTagPool.Add(entity) = new MoveTag();

                var eventAnim = _ecsWorldEvent.Value.NewEntity();
                var animatorView = _animatorViewPool.Value.Get(entity);
                animatorViewPool.Add(eventAnim) = animatorView;
                animatorTriggerPool.Add(eventAnim) = new AnimatorTrigger { Value = WalkAnimatorTrigger};
                animEventPool.Add(eventAnim) = new AnimEvent();
            }
        }
        
        private bool FindNearestEnemy(Vector3 currentPosition, float detectionRadius, int layerMask, out int nearestEnemy)
        {
            var closestDistance = float.MaxValue;
            nearestEnemy = -1;
            var hitColliders = Physics.OverlapSphere(currentPosition, detectionRadius, layerMask);

            foreach (var collider in hitColliders)
            {
                if (!collider.TryGetComponent<Entity>(out var entityTarget)) 
                    continue;
                
                if (entityTarget.HasData<Inactive>()) continue;

                var distance = Vector3.Distance(currentPosition, collider.transform.position);

                if (distance >= closestDistance) continue;
                
                closestDistance = distance;
                nearestEnemy = entityTarget.Id;
            }

            return nearestEnemy >= 0;
        }
    }
}