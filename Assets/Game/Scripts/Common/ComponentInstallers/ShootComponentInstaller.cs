using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class ShootComponentInstaller : IComponentInstaller, IAddExpression
    {
        [SerializeField] private BaseEvent attackAction;
        [SerializeField] private Countdown countdown;
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private AndExpression canAttack;

        //TODO Maybe move to AmmoInstaller ?
        [BoxGroup("Ammo"), SerializeField] private Const<int> maxAmmo;
        [BoxGroup("Ammo"), SerializeField] private ReactiveVariable<int> currentAmmo;
        [BoxGroup("Ammo"), SerializeField] private Transform firePoint;
        [BoxGroup("Ammo"), SerializeField] private Cycle cycle;
        [BoxGroup("Ammo"), SerializeField] private BaseEvent<Transform> shootAction;
        
        public void Install(IEntity entity)
        {
            entity.AddAttackCountdown(countdown);
            entity.AddAttackAction(attackAction);
            entity.AddCanAttack(canAttack);
            canAttack.Append(() => currentAmmo.Value > 0);

            entity.AddBehaviour(new ShootActionBehaviour());

            //TODO Move to AmmoInstaller ?
            entity.AddShootAction(shootAction);
            entity.AddMaxAmmo(maxAmmo);
            entity.AddCurrentAmmo(currentAmmo);
            entity.AddAttackPeroid(cycle);
            entity.AddFirePoint(firePoint);
            entity.AddBehaviour(new ShootAmmoBehaviour());
            
            //TODO Move to VisualInstaller
            entity.AddShootVFX(particleSystem);
            entity.AddBehaviour(new ShootVfxBehaviour());
        }

        public void Append(Func<bool> func)
        {
            canAttack.Append(func);
        }
    }
}