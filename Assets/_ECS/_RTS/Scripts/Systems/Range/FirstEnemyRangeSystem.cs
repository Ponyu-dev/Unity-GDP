using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Sirenix.Utilities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems.Range
{
    public class FirstEnemyRangeSystem : IEcsRunSystem
    {
        protected readonly EcsFilterInject<Inc<Position, MoveDirection, DetectorRange, Layer>> _filterEnemy;
        protected readonly EcsFilterInject<Inc<Position, Layer, Base>> _filterBase;
        
        public void Run(IEcsSystems systems)
        {
            var positionPool = _filterEnemy.Pools.Inc1;
            var moveDirectionPool = _filterEnemy.Pools.Inc2;
            var rangePool = _filterEnemy.Pools.Inc3;
            var layerPool = _filterEnemy.Pools.Inc4;

            foreach (var entityEnemy in _filterEnemy.Value)
            {
                var position = positionPool.Get(entityEnemy).Value;
                var range = rangePool.Get(entityEnemy).Value;
                var layer = layerPool.Get(entityEnemy).Value;
                
                ref var moveDirection = ref moveDirectionPool.Get(entityEnemy);
                var direction = moveDirection.Value;
                
                var colliders = Physics.OverlapSphere(position, range, layer);
                if (colliders.IsNullOrEmpty()) 
                {
                    var attackBasePosition = AttackBasePosition(position, layer);
                    if (attackBasePosition != Vector3.zero)
                        direction = attackBasePosition;
                }
                else
                    direction = (colliders[0].transform.position - position).normalized;
                
                moveDirection.Value = direction;
            }
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