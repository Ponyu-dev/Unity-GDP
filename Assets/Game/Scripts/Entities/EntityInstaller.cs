using Atomic.Entities;
using Atomic.Extensions;
using Game.Scripts.Common.Components;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public abstract class EntityInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private MovementComponent movementComponent;
        [SerializeField] private RotationComponent rotateComponent;
        
        public override void Install(IEntity entity)
        {
            entity.AddRootTransform(transform);
            
            entity.AddPosition(new float3Reactive(transform.position));
            entity.AddRotation(new quaternionReactive(transform.rotation));
            entity.AddBehaviour<TransformBehaviour>();
            
            movementComponent.Install(entity);
            rotateComponent.Install(entity);
        }
    }
}