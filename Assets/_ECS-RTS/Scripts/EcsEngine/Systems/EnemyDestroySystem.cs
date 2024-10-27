using System.Collections.Generic;
using System.Linq;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class EnemyDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TeamTag, EntityTag, AnimatorView>, Exc<Inactive>> _filterEnemy;
        private readonly EcsFilterInject<Inc<TargetEntity, DestroyEvent>> _filter = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _ecsWorldEvent = EcsWorlds.EVENTS;
        
        private readonly IReadOnlyList<IEnemiesFactory> _enemiesFactories;
        
        private static readonly int ADeathAnimatorTrigger = Animator.StringToHash("DeathA");
        private static readonly int BDeathAnimatorTrigger = Animator.StringToHash("DeathB");
        private readonly EcsFilterInject<Inc<AnimatorView, AnimatorTrigger, AnimEvent>> _filterAnimEvent = EcsWorlds.EVENTS;
        
        public EnemyDestroySystem(IReadOnlyList<IEnemiesFactory> enemiesFactories)
        {
            _enemiesFactories = enemiesFactories;
        }

        public void Run(IEcsSystems systems)
        {
            var targetEntityPool = _filter.Pools.Inc1;
            
            var teamTagPool = _filterEnemy.Pools.Inc1;
            var entityTagPool = _filterEnemy.Pools.Inc2;
            var animatorPool = _filterEnemy.Pools.Inc3;
            
            var animatorViewPool = _filterAnimEvent.Pools.Inc1;
            var animatorTriggerPool = _filterAnimEvent.Pools.Inc2;
            var animEventPool = _filterAnimEvent.Pools.Inc3;
            
            foreach (var @event in _filter.Value)
            {
                var id = targetEntityPool.Get(@event).Value;
                
                var randomIndex = Random.Range(0, 2);
                var trigger = randomIndex == 0 ? ADeathAnimatorTrigger : BDeathAnimatorTrigger;

                var animEvent = _ecsWorldEvent.Value.NewEntity();
                var animView = animatorPool.Get(id).Value;
                animatorViewPool.Add(animEvent) = new AnimatorView { Value = animView };
                animatorTriggerPool.Add(animEvent) = new AnimatorTrigger { Value = trigger };
                animEventPool.Add(animEvent) = new AnimEvent();

                var team = teamTagPool.Get(id).Value;
                var entityType = entityTagPool.Get(id).Value;
                Debug.Log($"[EnemyDestroySystem] {id} DeathEvent");
                _enemiesFactories.First(it => it.GetTeamType() == team).InactiveObject(entityType, id);
                
                _ecsWorldEvent.Value.DelEntity(@event);
            }
        }
    }
}