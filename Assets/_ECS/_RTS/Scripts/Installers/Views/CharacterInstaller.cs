using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Installers
{
    public class CharacterInstaller : EntityInstaller
    {
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private Transform moveDirection;
        
        protected override void Install(Entity entity)
        {
            Debug.Log($"[CharacterInstaller] Install({entity.Id})");
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new MoveDirection {Value = moveDirection.forward});
            entity.AddData(new MoveSpeed {Value = moveSpeed});
            entity.AddData(new TransformView { Value = transform});
        }

        protected override void Dispose(Entity entity) { }
    }
}