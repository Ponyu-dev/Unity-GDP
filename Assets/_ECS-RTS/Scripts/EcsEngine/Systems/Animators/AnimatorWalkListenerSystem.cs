using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems.Animators
{
    internal sealed class AnimatorWalkListenerSystem : IEcsRunSystem
    {
        private static readonly int WalkAnimatorTrigger = Animator.StringToHash("Walk");

        private readonly EcsFilterInject<Inc<AnimatorView, WalkEvent>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var animator = _filter.Pools.Inc1.Get(entity).Value;
                animator.SetTrigger(WalkAnimatorTrigger);
            }
        }
    }
}