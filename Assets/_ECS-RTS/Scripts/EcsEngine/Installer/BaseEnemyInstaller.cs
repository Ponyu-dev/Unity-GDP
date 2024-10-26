using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Installer
{
    [RequireComponent(typeof(Entity))]
    public abstract class BaseEnemyInstaller : EntityInstaller
    {
        [SerializeField] private LayerMask attackLayerMask;
        [SerializeField] private TeamType teamType;
        [SerializeField] private EntityType entityType;
        [SerializeField] private int health = 5;
        [SerializeField] private float moveSpeed = 5.0f;
        //[SerializeField] private float rangeFinder = 8.0f;
        [SerializeField] private float rangeAttacker = 4.0f;
        [SerializeField] private Animator animator;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Inactive());
            entity.AddData(new TeamTag {Value = teamType});
            entity.AddData(new EntityTag {Value = entityType});
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new MoveDirection {Value = Vector3.forward});
            entity.AddData(new MoveSpeed {Value = moveSpeed});
            entity.AddData(new TransformView {Value = transform});
            entity.AddData(new AnimatorView {Value = animator});
            entity.AddData(new Health {Value = health});
            entity.AddData(new SfxTakeDamage {Value = SfxType.Blood});
            //entity.AddData(new RangeFinder {Value = rangeFinder});
            entity.AddData(new RangeAttacker {Value = rangeAttacker});
            entity.AddData(new AttackLayerMaskView {Value = attackLayerMask});
            entity.AddData(new DamageableTag());
        }

        protected override void Dispose(Entity entity) { }
    }
}