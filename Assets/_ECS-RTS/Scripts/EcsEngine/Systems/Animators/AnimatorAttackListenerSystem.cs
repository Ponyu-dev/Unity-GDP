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

        private readonly EcsFilterInject<Inc<AttackEvent, SourceEntity>> _filter = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<AnimatorView> _animatorPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                var source = _filter.Pools.Inc2.Get(@event).Value;

                if (!_animatorPool.Value.Has(source)) continue;
                
                var randomIndex = Random.Range(0, 2);
                var animator = _animatorPool.Value.Get(source).Value;
                var trigger = randomIndex == 0 ? AAttackAnimatorTrigger : BAttackAnimatorTrigger;
                animator.SetTrigger(trigger);
            }
        }
    }
}