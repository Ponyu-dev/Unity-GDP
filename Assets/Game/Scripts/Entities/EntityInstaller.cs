using System;
using Atomic.Elements;
using Atomic.Entities;
using Atomic.Extensions;
using Game.Scripts.Common.Components;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public abstract class EntityInstaller : SceneEntityInstallerBase
    {
        [Header("Expressions")]
        [SerializeField] private AndExpression canTakeDamage;
        [SerializeField] private AndExpression canMove;
        [SerializeField] private AndExpression canRotate;
        
        [SerializeField] private LifeComponent lifeComponent;
        [SerializeField] private MovementComponent movementComponent;
        [SerializeField] private RotationComponent rotateComponent;
        [SerializeField] private AnimatorComponent animatorComponent;


        public override void Install(IEntity entity)
        {
            entity.AddRootTransform(transform);
            
            entity.AddPosition(new float3Reactive(transform.position));
            entity.AddRotation(new quaternionReactive(transform.rotation));
            entity.AddBehaviour<TransformBehaviour>();
            
            movementComponent.Install(entity);
            rotateComponent.Install(entity);
            lifeComponent.Install(entity);
            animatorComponent.Install(entity);
            
            entity.AddCanTakeDamage(canTakeDamage);
            entity.AddCanMove(canMove);
            entity.AddCanRotate(canRotate);
            
            canTakeDamage.Append(BaseCondition());
            canMove.Append(Condition());
            canRotate.Append(Condition());

            entity.AddBehaviour(new DeadBehaviour());
        }

        protected virtual Func<bool> Condition()
        {
            return BaseCondition();
        }

        protected Func<bool> BaseCondition()
        {
            return () => !lifeComponent.IsDead.Value;
        } 
    }
}