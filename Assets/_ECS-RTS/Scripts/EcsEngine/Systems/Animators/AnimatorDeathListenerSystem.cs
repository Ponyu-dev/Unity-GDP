using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Animators
{
    internal sealed class AnimatorDeathListenerSystem : IEcsRunSystem
    {
        private static readonly int ADeathAnimatorTrigger = Animator.StringToHash("DeathA");
        private static readonly int BDeathAnimatorTrigger = Animator.StringToHash("DeathB");

        private readonly EcsFilterInject<Inc<AnimatorView, DeathEvent>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var randomIndex = Random.Range(0, 2);
                var animator = _filter.Pools.Inc1.Get(entity).Value;
                var trigger = randomIndex == 0 ? ADeathAnimatorTrigger : BDeathAnimatorTrigger;
                animator.SetTrigger(trigger);
            }
        }
    }
}