using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Installer
{
    [RequireComponent(typeof(Entity))]
    public class BaseInstaller : EntityInstaller
    {
        [SerializeField] private TeamType teamType;
        [SerializeField] private int health = 200;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new FactoryTag());
            entity.AddData(new TeamTag {Value = teamType});
            entity.AddData(new Health {Value = health});
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new SfxTakeDamage { Value = SfxType.BuildingBurning } );
            entity.AddData(new SfxDestroy { Value = SfxType.BuildingDestroyed } );
        }

        protected override void Dispose(Entity entity) { }
    }
}