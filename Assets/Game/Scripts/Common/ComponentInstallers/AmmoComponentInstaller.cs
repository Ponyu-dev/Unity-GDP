// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-16
// <file>: AmmoComponentInstaller.cs
// ------------------------------------------------------------------------------

using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class AmmoComponentInstaller : IComponentInstaller
    {
        [SerializeField] private Const<int> maxAmmo;
        [SerializeField] private ReactiveVariable<int> currentAmmo;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Cycle cycle;
        
        //TODO Move to VisualInstaller
        [SerializeField] private ParticleSystem particleSystem;
        
        public void Install(IEntity entity)
        {
            entity.AddMaxAmmo(maxAmmo);
            entity.AddCurrentAmmo(currentAmmo);
            entity.AddAttackPeroid(cycle);
            entity.AddFirePoint(firePoint);
            entity.AddBehaviour(new ShootAmmoBehaviour());
            
            //TODO Move to VisualInstaller
            entity.AddShootVFX(particleSystem);
            entity.AddBehaviour(new ShootVfxBehaviour());
        }

        public Func<bool> IsNotEmptyAmmo()
        {
            return () => currentAmmo.Value > 0;
        }
    }
}