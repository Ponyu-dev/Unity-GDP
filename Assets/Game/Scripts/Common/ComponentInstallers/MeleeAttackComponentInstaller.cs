using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class MeleeAttackComponentInstaller : IComponentInstaller, IAddExpression
    {
        [SerializeField] private Cycle periodAttack;
        [SerializeField] private Const<int> damage = new(1);
        [SerializeField] private AndExpression canAttack;
        
        public void Install(IEntity entity)
        {
            entity.AddAttackPeroid(periodAttack);
            entity.AddDamage(damage);
            entity.AddCanAttack(canAttack);
            
            entity.AddBehaviour(new MeleeAttackBehaviour());
        }

        public void Append(Func<bool> func)
        {
            canAttack.Append(func);
        }
    }
}