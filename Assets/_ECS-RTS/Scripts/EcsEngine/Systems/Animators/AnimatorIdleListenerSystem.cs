using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Animators
{
    internal sealed class AnimatorIdleListenerSystem : IEcsRunSystem
    {
        private static readonly int IdleAnimatorTrigger = Animator.StringToHash("Idle");
        private readonly EcsFilterInject<Inc<IdleEvent, SourceEntity>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<AnimatorView> _animatorPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                var source = _filter.Pools.Inc2.Get(@event).Value;

                if (!_animatorPool.Value.Has(source)) continue;
                
                var animator = _animatorPool.Value.Get(source).Value;
                animator.SetTrigger(IdleAnimatorTrigger);
            }
        }
    }
}