using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Components;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public sealed class CharacterEntityInstaller : EntityInstaller
    {
        [SerializeField] private Camera cameraMain;
        [SerializeField] private ShootComponent shootComponent;
        [SerializeField] private AndExpression canAttack;

        public override void Install(IEntity entity)
        {
            base.Install(entity);
            entity.AddCharacterTag();
            entity.AddCameraMain(cameraMain);
            entity.AddCanAttack(canAttack);
            shootComponent.Install(entity);
            
            canAttack.Append(ShootCondition());
        }
        
        private Func<bool> ShootCondition()
        {
            return () => shootComponent.CurrentAmmo.Value > 0;
        }
    }
}