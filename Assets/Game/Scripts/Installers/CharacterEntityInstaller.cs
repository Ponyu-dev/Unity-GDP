using Atomic.Entities;
using Atomic.Extensions;
using Game.Scripts.Common.ComponentInstallers;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public sealed class CharacterEntityInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private Camera cameraMain;
        
        [SerializeField] private LifeComponentInstaller lifeComponent;
        [SerializeField] private MovementComponentInstaller movementComponent;
        [SerializeField] private RotationComponentInstaller rotateComponent;
        
        [SerializeField] private ShootComponentInstaller shootComponent;

        //TODO Maybe move to VisualInstaller.
        [SerializeField] private AnimatorComponentInstaller animatorComponent;
        
        public override void Install(IEntity entity)
        {
            entity.AddCharacterTag();
            entity.AddCameraMain(cameraMain);
            entity.AddRootTransform(transform);
            entity.AddPosition(new float3Reactive(transform.position));
            entity.AddRotation(new quaternionReactive(transform.rotation));

            entity.AddBehaviour<TransformBehaviour>();
            
            lifeComponent.Install(entity);
            movementComponent.Install(entity);
            rotateComponent.Install(entity);
            shootComponent.Install(entity);
            animatorComponent.Install(entity);
            
            movementComponent.Append(lifeComponent.IsNotDead());
            rotateComponent.Append(lifeComponent.IsNotDead());
            shootComponent.Append(lifeComponent.IsNotDead());
        }
    }
}