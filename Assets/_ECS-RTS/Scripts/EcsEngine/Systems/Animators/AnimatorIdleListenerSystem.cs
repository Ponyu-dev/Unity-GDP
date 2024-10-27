using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Animators
{
    internal sealed class AnimatorIdleListenerSystem : IEcsRunSystem
    {
        private static readonly int IdleAnimatorTrigger = Animator.StringToHash("Idle");

        private readonly EcsFilterInject<Inc<AnimatorView, IdleEvent>, Exc<Inactive>> _filter;

        public void Run(IEcsSystems systems)
        {
            var animatorViewPool = _filter.Pools.Inc1;
            foreach (var @event in _filter.Value)
            {
                var animator = animatorViewPool.Get(@event).Value;
                animator.SetTrigger(IdleAnimatorTrigger);
            }
        }
    }
}