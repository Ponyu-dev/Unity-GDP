using ShootEmUp;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    public class MainLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GameManager>().AsImplementedInterfaces();
            
            builder.Register<ShootInput>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MoveInput>(Lifetime.Singleton).AsImplementedInterfaces();

            RegisterCharacter(builder);
        }
        
        [SerializeField] private Character m_Character;
        [SerializeField] private DamageComponent m_DamageComponent;
        
        //TODO I couldn't take it out separately like I did with UILifetimeScope. Tell me if there is a way to do this.
        private void RegisterCharacter(IContainerBuilder builder)
        {
            Debug.Log("[CharacterInstaller] RegisterCharacter");
            
            builder.RegisterInstance(m_Character)
                .AsImplementedInterfaces();

            //Register Component
            builder.Register<HitPointsComponent>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(m_Character.hitPointsData);
            builder.Register<MoveComponent>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(m_Character.moveData);
            
            //Register Observer
            builder.Register<CharacterMoveObserver>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<CharacterDeathObserver>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<CharacterShootObserver>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter("weaponData", m_Character.weaponData);
            
            //Register Character DamageComponent
            builder.RegisterInstance(m_DamageComponent)
                .AsImplementedInterfaces()
                .WithParameter("teamData", m_Character.teamData);
        }
    }
}