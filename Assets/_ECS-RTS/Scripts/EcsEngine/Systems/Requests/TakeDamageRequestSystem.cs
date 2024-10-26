using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Services;
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
        private readonly EcsPoolInject<SfxTakeDamage> _sfxTakeDamagePool;
        private readonly EcsPoolInject<TakeDamageEvent> _takeDamagePool;

        private readonly ISfxFactory _sfxFactory;
        
        public TakeDamageRequestSystem(ISfxFactory sfxFactory)
        {
            _sfxFactory = sfxFactory;
        }

        public void Run(IEcsSystems systems)
        {
            var targetEntityPool = _filterRequest.Pools.Inc2;
            var damagePool = _filterRequest.Pools.Inc3;
            var pointBloodPool = _filterRequest.Pools.Inc4;
            
            foreach (var entity in _filterRequest.Value)
            {
                var targetId = targetEntityPool.Get(entity).Value;
                
                if (_takeDamagePool.Value.Has(targetId)) continue;
                if (!_healthPool.Value.Has(targetId)) continue;

                var damage = damagePool.Get(entity).Value;
                
                ref var health = ref _healthPool.Value.Get(targetId).Value;
                health = Mathf.Max(0, health - damage);

                _takeDamagePool.Value.Add(targetId) = new TakeDamageEvent();

                var pointBlood = pointBloodPool.Get(entity).Value;
                _sfxFactory.PlaySfx(_sfxTakeDamagePool.Value.Get(targetId).Value, pointBlood);                
                
                _worldEvent.Value.DelEntity(entity);
            }
        }
    }
}