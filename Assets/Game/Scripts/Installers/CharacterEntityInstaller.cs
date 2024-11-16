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
        
        [SerializeField] private LifeComponentInstaller lifeComponentInstaller;
        [SerializeField] private MovementComponentInstaller movementComponentInstaller;
        [SerializeField] private RotationComponentInstaller rotateComponentInstaller;
        
        [SerializeField] private ShootComponentInstaller shootComponentInstaller;
        [SerializeField] private AmmoComponentInstaller ammoComponentInstaller;

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
            
            lifeComponentInstaller.Install(entity);
            movementComponentInstaller.Install(entity);
            rotateComponentInstaller.Install(entity);
            shootComponentInstaller.Install(entity);
            ammoComponentInstaller.Install(entity);
            animatorComponent.Install(entity);
            
            movementComponentInstaller.Append(lifeComponentInstaller.IsNotDead());
            rotateComponentInstaller.Append(lifeComponentInstaller.IsNotDead());
            shootComponentInstaller.Append(lifeComponentInstaller.IsNotDead());
            shootComponentInstaller.Append(ammoComponentInstaller.IsNotEmptyAmmo());
        }
    }
}