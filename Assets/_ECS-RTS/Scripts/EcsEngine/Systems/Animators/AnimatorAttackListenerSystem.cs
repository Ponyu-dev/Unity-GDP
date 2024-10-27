using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Animators
{
    internal sealed class AnimatorAttackListenerSystem : IEcsRunSystem
    {
        private static readonly int AAttackAnimatorTrigger = Animator.StringToHash("AttackA");
        private static readonly int BAttackAnimatorTrigger = Animator.StringToHash("AttackB");

        private readonly EcsFilterInject<Inc<AnimatorView, AttackEvent>, Exc<Inactive>> _filter;

        public void Run(IEcsSystems systems)
        {
            var animatorViewPool = _filter.Pools.Inc1;
            foreach (var @event in _filter.Value)
            {
                var animator = animatorViewPool.Get(@event).Value;
                
                var randomIndex = Random.Range(0, 2);
                var trigger = randomIndex == 0 ? AAttackAnimatorTrigger : BAttackAnimatorTrigger;
                animator.SetTrigger(trigger);
            }
        }
    }
}