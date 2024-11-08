using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.Components
{
    [Serializable]
    public sealed class MeleeAttackComponent : IComponentInstaller
    {
        [SerializeField] private Cycle periodAttack;
        
        public void Install(IEntity entity)
        {
            entity.AddAttackPeroid(periodAttack);

            entity.AddBehaviour(new MeleeAttackBehaviour());
        }
    }
}