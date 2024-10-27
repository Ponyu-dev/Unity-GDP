using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class DeathRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetEntity, DestroyEvent>> _filterEvent = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _ecsWorldEvent = EcsWorlds.EVENTS;
        
        private readonly EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<Inactive> _inactivePool;

        private readonly EcsFilterInject<Inc<MoveTag, MoveTarget, EntityTag>, Exc<Inactive>> _filterMoveTags;
        private readonly EcsFilterInject<Inc<AttackTag, AttackTargetEntity, EntityTag>, Exc<Inactive>> _filterAttackTags;

        private readonly EcsPoolInject<FinderNearestTargetRequest> _firstTargetSelectedPool;

        public void Run(IEcsSystems systems)
        {
            var filterPool = _filter.Pools.Inc1;

            var targetEntityPool = _filterEvent.Pools.Inc1;
            var destroyEventPool = _filterEvent.Pools.Inc2;
            
            foreach (var entity in _filter.Value)
            {
                filterPool.Del(entity);
                
                Debug.Log($"[DeathRequestSystem] {entity} DeathEvent");

                var eventId = _ecsWorldEvent.Value.NewEntity();
                targetEntityPool.Add(eventId) = new TargetEntity { Value = entity };
                destroyEventPool.Add(eventId) = new DestroyEvent();
                
                _inactivePool.Value.Add(entity) = new Inactive();

                ProcessTags(entity, _filterMoveTags, _firstTargetSelectedPool.Value);
                ProcessTags(entity, _filterAttackTags, _firstTargetSelectedPool.Value);
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