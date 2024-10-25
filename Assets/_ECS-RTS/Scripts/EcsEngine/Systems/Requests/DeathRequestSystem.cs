using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

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
            var moveTagPool = _filterMoveTags.Pools.Inc1;
            var moveTargetPool = _filterMoveTags.Pools.Inc2;
            
            var attackTagPool = _filterAttackTags.Pools.Inc1;
            var attackTargetEntityPool = _filterAttackTags.Pools.Inc2;
            
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);

                _inactivePool.Value.Add(entity) = new Inactive();
                _eventPool.Value.Add(entity) = new DeathEvent();
                
                foreach (var entityTags in _filterAttackTags.Value)
                {
                    if (!attackTagPool.Has(entityTags) || attackTargetEntityPool.Get(entityTags).Value != entity)
                        continue;
                    
                    attackTagPool.Del(entityTags);
                    attackTargetEntityPool.Del(entityTags);
                    
                    _firstTargetSelectedPool.Value.Add(entityTags) = new FinderNearestTargetRequest();
                }
                
                foreach (var entityTags in _filterMoveTags.Value)
                {
                    if (!moveTagPool.Has(entityTags) || attackTargetEntityPool.Get(entityTags).Value != entity)
                        continue;
                    
                    moveTagPool.Del(entityTags);
                    moveTargetPool.Del(entityTags);
                    
                    _firstTargetSelectedPool.Value.Add(entityTags) = new FinderNearestTargetRequest();
                }
            }
        }
    }
}