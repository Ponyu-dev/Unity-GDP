using Atomic.Entities;
using Atomic.Extensions;
using Game.Scripts.Common.Mechanics;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public sealed class CharacterEntityInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private Camera cameraMain;
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float angularSpeed = 5f;
        [SerializeField] private float3 initialDirection;

        public override void Install(IEntity entity)
        {
            entity.AddCharacterTag();
            entity.AddRootTransform(transform);
            entity.AddCameraMain(cameraMain);
            
            entity.AddPosition(new float3Reactive(transform.position));
            entity.AddRotation(new quaternionReactive(transform.rotation));
            entity.AddBehaviour<TransformBehaviour>();

            entity.AddMoveSpeed(moveSpeed);
            entity.AddMoveDirection(initialDirection);
            entity.AddBehaviour<MovementBehaviour>();

            entity.AddLook(initialDirection);
            entity.AddAngularSpeed(angularSpeed);
            entity.AddBehaviour<RotationBehaviour>();
        }
    }
}