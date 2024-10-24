using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Installer
{
    public abstract class BaseEnemyInstaller : EntityInstaller
    {
        [SerializeField] private TeamType teamType;
        [SerializeField] private EntityType entityType;
        [SerializeField] private int health = 5;
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private Animator animator;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new TeamTag {Value = teamType});
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new MoveDirection {Value = Vector3.forward});
            entity.AddData(new MoveSpeed {Value = moveSpeed});
            entity.AddData(new TransformView { Value = transform});
            entity.AddData(new AnimatorView { Value = animator});
            entity.AddData(new Health { Value = health});
            entity.AddData(new DamageableTag());
        }

        protected override void Dispose(Entity entity) { }
    }
}