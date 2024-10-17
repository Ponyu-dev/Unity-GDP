using _ECS._RTS.Scripts.Components;
using _ECS._RTS.Scripts.Components.Range;
using Sirenix.Utilities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems.Range
{
    public class AttackRangeSystem : BaseRangeSystem<IsMoving, AttackRange>
    {
        protected override void HandleColliders(int entity, Collider[] colliders)
        {
            var isMovingPool = _filter.Pools.Inc2;
            ref var isMoving = ref isMovingPool.Get(entity);
            isMoving.Value = colliders.IsNullOrEmpty();
        }
    }
}