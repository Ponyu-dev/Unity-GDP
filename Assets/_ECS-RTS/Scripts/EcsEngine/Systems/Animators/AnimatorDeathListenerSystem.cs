using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Animators
{
    internal sealed class AnimatorDeathListenerSystem : IEcsRunSystem
    {
        private static readonly int DeathAnimatorTrigger = Animator.StringToHash("DeathA");
        //private static readonly int DeathAnimatorTrigger = Animator.StringToHash("DeathB");

        private readonly EcsFilterInject<Inc<AnimatorView, DeathEvent>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var animator = _filter.Pools.Inc1.Get(entity).Value;
                animator.SetTrigger(DeathAnimatorTrigger);
            }
        }
    }
}