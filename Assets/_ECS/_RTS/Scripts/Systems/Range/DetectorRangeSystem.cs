using _ECS._RTS.Scripts.Components;
using _ECS._RTS.Scripts.Components.Range;
using Sirenix.Utilities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems.Range
{
    public class DetectorRangeSystem : BaseRangeSystem<MoveDirection, DetectorRange>
    {
        protected override void HandleColliders(int entity, Collider[] colliders)
        {
            if (colliders.IsNullOrEmpty()) return;

            var positionPool = _filter.Pools.Inc1;
            var moveDirectionPool = _filter.Pools.Inc2;

            var position = positionPool.Get(entity).Value;  // Позиция текущего объекта
            var enemyPosition = colliders[0].transform.position;  // Позиция первого врага

            // Вычисляем направление к врагу и нормализуем его
            var directionToEnemy = (enemyPosition - position).normalized;

            // Устанавливаем направление движения к врагу
            ref var moveDirection = ref moveDirectionPool.Get(entity);
            moveDirection.Value = directionToEnemy;
        }
    }
}