using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using Game.Scripts.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Common.Components
{
    [Serializable]
    public sealed class ShootComponent : IComponentInstaller
    {
        [SerializeField] private AnimationEventsHandler animationEventsHandler;
        [SerializeField] private BaseEvent attackAction;
        [SerializeField] private Countdown countdown;
        [SerializeField] private ParticleSystem particleSystem;

        [BoxGroup("Ammo"), SerializeField] private Const<int> maxAmmo;
        [BoxGroup("Ammo"), SerializeField] private ReactiveVariable<int> currentAmmo;
        [BoxGroup("Ammo"), SerializeField] private Transform firePoint;
        [BoxGroup("Ammo"), SerializeField] private Cycle cycle;
        [BoxGroup("Ammo"), SerializeField] private BaseEvent<Transform> shootAction;

        public IValue<int> CurrentAmmo => currentAmmo;
        
        public void Install(IEntity entity)
        {
            entity.AddShootAnimationReceiver(animationEventsHandler);
            entity.AddAttackCountdown(countdown);
            entity.AddAttackAction(attackAction);
            entity.AddShootAction(shootAction);
            entity.AddShootFX(particleSystem);
            entity.AddMaxAmmo(maxAmmo);
            entity.AddCurrentAmmo(currentAmmo);
            entity.AddAttackPeroid(cycle);
            entity.AddFirePoint(firePoint);

            entity.AddBehaviour(new ShootActionBehaviour());
            entity.AddBehaviour(new ShootAmmoBehaviour());
            entity.AddBehaviour(new ShootSfxBehaviour());
        }
    }
}