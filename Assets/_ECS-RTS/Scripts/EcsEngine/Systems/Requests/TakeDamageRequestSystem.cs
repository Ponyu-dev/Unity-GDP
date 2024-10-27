using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class TakeDamageRequestSystem : IEcsRunSystem
    {
        private static readonly int TakeDamageAnimatorTrigger = Animator.StringToHash("TakeDamage");
        private readonly EcsPoolInject<AnimatorView> _animatorViewPool;
        private readonly EcsFilterInject<Inc<AnimatorView, AnimatorTrigger, AnimEvent>> _filterEvent = EcsWorlds.EVENTS;
        
        private readonly EcsFilterInject<Inc<TakeDamageRequest, TargetEntity, Damage, Position>> _filterRequest = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _worldEvent = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<SfxTakeDamage> _sfxTakeDamagePool;

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
            
            var animatorViewPool = _filterEvent.Pools.Inc1;
            var animatorTriggerPool = _filterEvent.Pools.Inc2;
            var animEventPool = _filterEvent.Pools.Inc3;
            
            foreach (var @event in _filterRequest.Value)
            {
                var targetId = targetEntityPool.Get(@event).Value;
                
                if (!_healthPool.Value.Has(targetId)) continue;

                var damage = damagePool.Get(@event).Value;
                
                ref var health = ref _healthPool.Value.Get(targetId).Value;
                health = Mathf.Max(0, health - damage);
                
                Debug.Log($"[TakeDamageRequestSystem] Run targetId = {targetId} health = {health}");

                var animatorView = _animatorViewPool.Value.Get(targetId);
                
                var eventAnim = _worldEvent.Value.NewEntity();
                animatorViewPool.Add(eventAnim) = animatorView;
                animatorTriggerPool.Add(eventAnim) = new AnimatorTrigger { Value = TakeDamageAnimatorTrigger};
                animEventPool.Add(eventAnim) = new AnimEvent();
                
                var pointBlood = pointBloodPool.Get(@event).Value;
                _sfxFactory.PlaySfx(_sfxTakeDamagePool.Value.Get(targetId).Value, pointBlood);               
                
                _worldEvent.Value.DelEntity(@event);
            }
        }
    }
}