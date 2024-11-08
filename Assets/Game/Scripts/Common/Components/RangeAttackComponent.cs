using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.Components
{
    [Serializable]
    public sealed class RangeAttackComponent : IComponentInstaller
    {
        [SerializeField] private float range;
        [SerializeField] private ReactiveVariable<bool> isRangeAttack;
        
        public void Install(IEntity entity)
        {
            entity.AddAttackRange(new Const<float>(range));
            entity.AddBehaviour(new RangeAttackBehaviour());
            entity.AddIsRangeAttack(isRangeAttack);
        }
        
        public Func<bool> ConditionRange()
        {
            return () => !isRangeAttack.Value;
        } 
    }
}