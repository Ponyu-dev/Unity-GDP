using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class CollisionRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionEnterRequest, Damage, TargetEntity, Position, CollisionEnterTag>> _filterRequest 
            = EcsWorlds.EVENTS;
        private readonly EcsFactoryInject<TakeDamageRequest, TargetEntity, Damage, Position> _takeDamageEmitter 
            = EcsWorlds.EVENTS;
        
        private readonly EcsWorldInject _worldEvent = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<DamageableTag> _damageableTagPool;
        
        public void Run(IEcsSystems systems)
        {
            var collisionEnterRequestPool = _filterRequest.Pools.Inc1;
            var damagePool = _filterRequest.Pools.Inc2;
            var targetEntityPool = _filterRequest.Pools.Inc3;
            var positionContactPool = _filterRequest.Pools.Inc4;
            
            foreach (var id in _filterRequest.Value)
            {
                collisionEnterRequestPool.Del(id);
                
                var targetId = targetEntityPool.Get(id);
                var damage = damagePool.Get(id);
                
                if (!_damageableTagPool.Value.Has(targetId.Value)) continue;
                
                _takeDamageEmitter.Value.NewEntity(
                    new TakeDamageRequest(),
                    targetId,
                    damage,
                    positionContactPool.Get(id)
                );
                Debug.Log($"[CollisionRequestSystem] Run {targetId.Value} damage {damage.Value}");
                
                _worldEvent.Value.DelEntity(id);
            }
        }
    }
}