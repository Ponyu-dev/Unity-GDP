using _ECS._RTS.Scripts.Components.Anim;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems
{
    public class AnimationSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<AnimEvent, AnimView, AnimData>> _filter = EcsWorlds.EVENTS;

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                var animator = _filter.Pools.Inc2.Get(@event).Value;
                var animationData = _filter.Pools.Inc3.Get(@event).Value;
                
                Debug.Log($"[AnimationSystem] Run {animationData.Animation}");
                animator.PlayAsync(animationData);

                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}