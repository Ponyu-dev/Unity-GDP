using System;
using ShootEmUp;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    [Serializable]
    public sealed class CharacterLifetimeScope
    {
        [SerializeField] private Character character;
        [SerializeField] private DamageComponent damageComponent;
        [SerializeField] private BulletConfig bulletConfig;
        
        public void Configure(IContainerBuilder builder)
        {
            Debug.Log("[CharacterLifetimeScope] Configure");
            
            builder.RegisterInstance(character)
                .AsImplementedInterfaces();

            //Register Character Component
            builder.Register<HitPointsComponent>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(character.hitPointsData);
            builder.Register<MoveComponent>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(character.moveData);
            
            //Register Character Observer
            builder.Register<CharacterMoveObserver>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<CharacterDeathObserver>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<CharacterShootObserver>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(typeof(BulletConfig), bulletConfig)
                .WithParameter(typeof(WeaponData), character.weaponData);
            
            //Register Character DamageComponent
            builder.RegisterComponent(damageComponent)
                .WithParameter("teamData", character.teamData);
        }
    }
}