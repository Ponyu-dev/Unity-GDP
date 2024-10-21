using System.Collections.Generic;
using _ECS._RTS.Scripts.AnimationHelper.Base;
using _ECS._RTS.Scripts.Components;
using _ECS._RTS.Scripts.Components.Anim;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Sirenix.Utilities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems.Range
{
    public class NearestEnemyRangeSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        
        protected readonly EcsFilterInject<Inc<Position, MoveDirection, DetectorRange, Layer, AnimView, Attacking, Rotation>> _filterEnemy;
        protected readonly EcsFilterInject<Inc<Position, Layer, Base>> _filterBase;
        
        private readonly EcsPoolInject<AnimEvent> _animEventPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<AnimView> _animViewPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<AnimData> _animDataPool = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            var positionPool = _filterEnemy.Pools.Inc1;
            var moveDirectionPool = _filterEnemy.Pools.Inc2;
            var rangePool = _filterEnemy.Pools.Inc3;
            var layerPool = _filterEnemy.Pools.Inc4;
            var animViewPool = _filterEnemy.Pools.Inc5;
            var attackingPool = _filterEnemy.Pools.Inc6;
            var rotationPool = _filterEnemy.Pools.Inc7;

            foreach (var entityEnemy in _filterEnemy.Value)
            {
                var attack = attackingPool.Get(entityEnemy).Value;
                Debug.Log($"[NearestEnemyRangeSystem] attack {entityEnemy} {attack}");
                if (attack) continue;
                
                var position = positionPool.Get(entityEnemy).Value;
                var range = rangePool.Get(entityEnemy).Value;
                var layer = layerPool.Get(entityEnemy).Value;
                var animView = animViewPool.Get(entityEnemy).Value;

                var direction = AttackBasePosition(position, layer);

                var colliders = Physics.OverlapSphere(position, range, layer);
                if (!colliders.IsNullOrEmpty())
                {
                    direction = (SelectTargetEnemy(position, colliders) - position).normalized;
                    AnimEvent(animView, Animations.RUN);
                }
                else
                {
                    AnimEvent(animView, Animations.WALK);
                }
                
                ref var moveDirection = ref moveDirectionPool.Get(entityEnemy);
                moveDirection.Value = direction;
                
                ref var rotation = ref rotationPool.Get(entityEnemy);
                rotation.Value = Quaternion.LookRotation(direction);
            }
        }

        private void AnimEvent(IAnimatorCoder animatorCoder, Animations animations)
        {
            var animEvent = _eventWorld.Value.NewEntity();

            _animEventPool.Value.Add(animEvent) = new AnimEvent(); 
            _animViewPool.Value.Add(animEvent) = new AnimView { Value = animatorCoder };
            _animDataPool.Value.Add(animEvent) = new AnimData { Value = new AnimationData(animations)};
        }
        
        private Vector3 SelectTargetEnemy(Vector3 currentPosition, IEnumerable<Collider> colliders)
        {
            Collider nearestEnemy = null;
            var nearestDistance = float.MaxValue;

            foreach (var collider in colliders)
            {
                var enemyPosition = collider.transform.position;
                var distance = Vector3.Distance(currentPosition, enemyPosition);
                if (!(distance < nearestDistance)) continue;
                
                nearestEnemy = collider;
                nearestDistance = distance;
            }

            return nearestEnemy != null ? nearestEnemy.transform.position : Vector3.zero;
        }
        
        private Vector3 AttackBasePosition(Vector3 enemyPosition, int attackLayer)
        {
            var positionBasePool = _filterBase.Pools.Inc1;
            var layerBasePool = _filterBase.Pools.Inc2;
            var targetPosition = Vector3.zero;
            
            foreach (var baseEntity in _filterBase.Value)
            {
                var positionBase = positionBasePool.Get(baseEntity).Value;
                var layerBase = layerBasePool.Get(baseEntity).Value;
                if (layerBase.value == attackLayer)
                    targetPosition = positionBase;
            }
            
            return (targetPosition - enemyPosition).normalized;
        }
    }
}