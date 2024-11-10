using System;
using Atomic.Entities;
using Game.Scripts.Common.Components;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public sealed class ZombieEntityInstaller : EntityInstaller
    {
        [SerializeField] private RangeAttackComponent rangeAttackComponent;
        [SerializeField] private MeleeAttackComponent meleeAttackComponent;
        [SerializeField] private TriggerEventComponent triggerEventComponent;
        
        public override void Install(IEntity entity)
        {
            base.Install(entity);
            entity.AddZombieTag();
            
            rangeAttackComponent.Install(entity);
            meleeAttackComponent.Install(entity);
            triggerEventComponent.Install(entity);
        }

        protected override Func<bool> Condition()
        {
            return rangeAttackComponent.ConditionRange();
        }
    }
}