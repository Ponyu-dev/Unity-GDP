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
        [SerializeField] private LifeComponent lifeComponent;
        [SerializeField] private MovementComponent movementComponent;
        [SerializeField] private RotationComponent rotateComponent;

        [Header("Expressions")]
        [SerializeField] private AndExpression canTakeDamage;
        [SerializeField] private AndExpression canMove;
        [SerializeField] private AndExpression canRotate;

        public override void Install(IEntity entity)
        {
            entity.AddRootTransform(transform);
            
            entity.AddPosition(new float3Reactive(transform.position));
            entity.AddRotation(new quaternionReactive(transform.rotation));
            entity.AddBehaviour<TransformBehaviour>();
            
            movementComponent.Install(entity);
            rotateComponent.Install(entity);
            lifeComponent.Install(entity);
            
            entity.AddCanTakeDamage(canTakeDamage);
            entity.AddCanMove(canMove);
            entity.AddCanRotate(canRotate);
            
            canTakeDamage.Append(() => !lifeComponent.IsDead.Value);
            canMove.Append(() => !lifeComponent.IsDead.Value);
            canRotate.Append(() => !lifeComponent.IsDead.Value);
        }
    }
}