using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class DeathRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> _filter;

        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsPoolInject<DeathEvent> _eventPool;

        private readonly EcsFilterInject<Inc<MoveTag, MoveTarget, EntityTag>, Exc<Inactive>> _filterMoveTags;
        private readonly EcsFilterInject<Inc<AttackTag, AttackTargetEntity, EntityTag>, Exc<Inactive>> _filterAttackTags;

        private readonly EcsPoolInject<FinderNearestTargetRequest> _firstTargetSelectedPool;

        public void Run(IEcsSystems systems)
        {
            var filter = _filter.Value;
            var inactivePool = _inactivePool.Value;
            var eventPool = _eventPool.Value;
            var firstTargetSelectedPool = _firstTargetSelectedPool.Value;
            
            foreach (var entity in filter)
            {
                _filter.Pools.Inc1.Del(entity);
                
                Debug.Log($"[DeathRequestSystem] {entity} DeathEvent");

                eventPool.Add(entity) = new DeathEvent();
                inactivePool.Add(entity) = new Inactive();

                ProcessTags(entity, _filterMoveTags, firstTargetSelectedPool);
                ProcessTags(entity, _filterAttackTags, firstTargetSelectedPool);
            }
        }

        private void ProcessTags<TTag, TTarget>(int entity, EcsFilterInject<Inc<TTag, TTarget, EntityTag>, Exc<Inactive>> filter, EcsPool<FinderNearestTargetRequest> requestPool)
            where TTag : struct 
            where TTarget : struct, ITargetEntity
        {
            var tagPool = filter.Pools.Inc1;
            var targetPool = filter.Pools.Inc2;

            foreach (var entityTag in filter.Value)
            {
                if (!tagPool.Has(entityTag) || targetPool.Get(entityTag).Value != entity)
                    continue;
                
                tagPool.Del(entityTag);
                targetPool.Del(entityTag);
                requestPool.Add(entityTag) = new FinderNearestTargetRequest();
            }
        }

    }
}