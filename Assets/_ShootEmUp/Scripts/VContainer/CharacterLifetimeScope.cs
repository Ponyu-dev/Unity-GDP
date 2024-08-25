using System;
using ShootEmUp;
using UnityEngine;
using VContainer;

namespace _ShootEmUp.Scripts.VContainer
{
    [Serializable]
    public sealed class CharacterLifetimeScope
    {
        [SerializeField] private Character character;
        [SerializeField] private DamageComponent damageComponent;
        [SerializeField] private BulletConfig m_BulletConfig;
        
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
            /*builder.Register<CharacterShootObserver>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter("bulletConfig", m_BulletConfig)
                .WithParameter("weaponData", character.weaponData);
                */
            
            //Register Character DamageComponent
            builder.RegisterInstance(damageComponent)
                .AsImplementedInterfaces()
                .WithParameter("teamData", character.teamData);
        }
    }
}