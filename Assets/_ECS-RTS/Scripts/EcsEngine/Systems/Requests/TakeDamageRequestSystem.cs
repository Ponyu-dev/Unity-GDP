using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class TakeDamageRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageRequest, TargetEntity, Damage, Position>> _filterRequest = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _worldEvent = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<TakeDamageEvent> _animatorViewPool;
        
        public void Run(IEcsSystems systems)
        {
            var targetEntityPool = _filterRequest.Pools.Inc2;
            var damagePool = _filterRequest.Pools.Inc3;
            var positionPool = _filterRequest.Pools.Inc4;
            
            foreach (var entity in _filterRequest.Value)
            {
                var targetId = targetEntityPool.Get(entity).Value;
                
                if (!_healthPool.Value.Has(targetId)) continue;

                var damage = damagePool.Get(entity).Value;
                
                Debug.Log($"[TakeDamageRequestSystem] Run {targetId} damage {damage}");
                
                ref var health = ref _healthPool.Value.Get(targetId).Value;
                health = Mathf.Max(0, health - damage);

                _animatorViewPool.Value.Add(targetId) = new TakeDamageEvent();
                
                
                _worldEvent.Value.DelEntity(entity);
            }
        }
    }
}