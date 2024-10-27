using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class AnimatorListenerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AnimatorView, AnimatorTrigger, AnimEvent>> _filterEvent = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _ecsWorldEvent = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            var animatorViewPool = _filterEvent.Pools.Inc1;
            var animatorTriggerPool = _filterEvent.Pools.Inc2;
            
            foreach (var @event in _filterEvent.Value)
            {
                var animator = animatorViewPool.Get(@event).Value;
                var trigger = animatorTriggerPool.Get(@event).Value;
                animator.SetTrigger(trigger);
                
                _ecsWorldEvent.Value.DelEntity(@event);
            }
        }
    }
}