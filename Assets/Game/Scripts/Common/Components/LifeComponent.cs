using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.Components
{
    [Serializable]
    public sealed class LifeComponent : IComponentInstaller
    {
        [SerializeField] private ReactiveVariable<int> hitPoints;
        [SerializeField] private ReactiveVariable<bool> isDead;
        [SerializeField] private BaseEvent<int> takeDamageAction;
        public IReactiveVariable<bool> IsDead => isDead;
        
        public void Install(IEntity entity)
        {
            entity.AddHitPoints(hitPoints);
            entity.AddIsDead(isDead);
            entity.AddTakeDamageAction(takeDamageAction);
            
            entity.AddBehaviour(new TakeDamageBehaviour());
        }
    }
}