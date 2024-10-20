using _ECS._RTS.Scripts.AnimationHelper.Base;
using _ECS._RTS.Scripts.Components;
using _ECS._RTS.Scripts.Components.Anim;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems
{
    public class AttackSystem : IEcsRunSystem
    {
        protected readonly EcsFilterInject<Inc<Attacking, AnimView>> _filter;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<AnimEvent> _animEventPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<AnimView> _animViewPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<AnimData> _animDataPool = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            var attackingPool = _filter.Pools.Inc1;
            var animViewPool = _filter.Pools.Inc2;
            
            foreach (var entity in _filter.Value)
            {
                var attacking = attackingPool.Get(entity);
                Debug.Log($"[AttackSystem] Run {entity} {attacking.Value}");

                if (!attacking.Value) continue;
                
                var animView = animViewPool.Get(entity).Value;
                
                if (animView.GetCurrentAnimation(0) == Animations.ATTACK1 ||
                    animView.GetCurrentAnimation(0) == Animations.ATTACK2)
                    continue;
                
                var spawnEvent = _eventWorld.Value.NewEntity();
                _animEventPool.Value.Add(spawnEvent) = new AnimEvent();
                _animViewPool.Value.Add(spawnEvent) = new AnimView { Value = animView };
                _animDataPool.Value.Add(spawnEvent) = new AnimData
                {
                    Value = new AnimationData(Animations.ATTACK1)
                };
            }
        }
    }
}