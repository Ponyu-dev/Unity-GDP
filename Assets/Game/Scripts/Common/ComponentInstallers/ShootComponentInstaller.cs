using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class ShootComponentInstaller : IComponentInstaller, IAddExpression
    {
        [SerializeField] private BaseEvent attackAction;
        [SerializeField] private Countdown countdown;
        [SerializeField] private AndExpression canAttack;
        [SerializeField] private BaseEvent<Transform> shootAction;
        
        public void Install(IEntity entity)
        {
            entity.AddAttackCountdown(countdown);
            entity.AddAttackAction(attackAction);
            entity.AddCanAttack(canAttack);
            entity.AddShootAction(shootAction);

            entity.AddBehaviour(new ShootActionBehaviour());
        }

        public void Append(Func<bool> func)
        {
            canAttack.Append(func);
        }
    }
}