using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class LifeComponentInstaller : IComponentInstaller
    {
        [SerializeField] private ReactiveVariable<int> hitPoints;
        [SerializeField] private ReactiveVariable<bool> isDead;
        [SerializeField] private BaseEvent<int> takeDamageAction;
        [SerializeField] private BaseEvent<IEntity> deadAction;
        [SerializeField] private AndExpression canTakeDamage;
        
        public void Install(IEntity entity)
        {
            entity.AddHitPoints(hitPoints);
            entity.AddIsDead(isDead);
            entity.AddTakeDamageAction(takeDamageAction);
            entity.AddDeadAction(deadAction);
            entity.AddCanTakeDamage(canTakeDamage);
            
            canTakeDamage.Append(IsNotDead());
            
            entity.AddBehaviour(new TakeDamageBehaviour());
            entity.AddBehaviour(new DeadBehaviour());
        }

        public Func<bool> IsNotDead()
        {
            return () => !isDead.Value;
        }
    }
}