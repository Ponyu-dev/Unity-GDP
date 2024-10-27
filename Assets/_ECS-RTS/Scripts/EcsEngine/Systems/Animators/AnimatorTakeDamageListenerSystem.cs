using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Animators
{
    internal sealed class AnimatorTakeDamageListenerSystem : IEcsRunSystem
    {
        private static readonly int TakeDamageAnimatorTrigger = Animator.StringToHash("TakeDamage");

        private readonly EcsFilterInject<Inc<AnimatorView, TakeDamageEvent>, Exc<Inactive>> _filter;

        public void Run(IEcsSystems systems)
        {
            var animatorViewPool = _filter.Pools.Inc1;
            foreach (var @event in _filter.Value)
            {
                var animator = animatorViewPool.Get(@event).Value;
                animator.SetTrigger(TakeDamageAnimatorTrigger);
            }
        }
    }
}