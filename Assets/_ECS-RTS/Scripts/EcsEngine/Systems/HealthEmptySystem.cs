using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class HealthEmptySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>, Exc<DeathRequest, Inactive>> _filter;
        
        private readonly EcsPoolInject<DeathRequest> _deathPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var health = _filter.Pools.Inc1.Get(entity);

                if (health.Value > 0) continue;
                
                _deathPool.Value.Add(entity) = new DeathRequest();
            }
        }
    }
}