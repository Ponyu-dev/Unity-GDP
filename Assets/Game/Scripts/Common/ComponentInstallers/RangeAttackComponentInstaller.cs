using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class RangeAttackComponentInstaller : IComponentInstaller
    {
        [SerializeField] private Const<float> range = new(1);
        [SerializeField] private ReactiveVariable<bool> isRangeAttack;

        public Func<bool> IsRangeAttack()
        {
            return () => isRangeAttack.Value;
        }

        public void Install(IEntity entity)
        {
            entity.AddAttackRange(range);
            entity.AddBehaviour(new RangeAttackBehaviour());
            
            entity.AddIsRangeAttack(isRangeAttack);
        }
    }
}