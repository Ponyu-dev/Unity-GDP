using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Animators
{
    internal sealed class AnimatorTakeDamageListenerSystem : IEcsRunSystem
    {
        private static readonly int TakeDamageAnimatorTrigger = Animator.StringToHash("TakeDamage");

        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> _filter = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<AnimatorView> _animatorPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                var target = _filter.Pools.Inc2.Get(@event).Value;

                if (!_animatorPool.Value.Has(target)) continue;
                
                var animator = _animatorPool.Value.Get(target).Value;
                animator.SetTrigger(TakeDamageAnimatorTrigger);
            }
        }
    }
}