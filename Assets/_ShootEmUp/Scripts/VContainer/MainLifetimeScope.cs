using ShootEmUp;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    public class MainLifetimeScope : LifetimeScope
    {
        [SerializeField] private UILifetimeScope m_UILifetimeScope;
        [SerializeField] private CharacterLifetimeScope m_CharacterLifetimeScope;
        [SerializeField] private BulletSpawnerLifetimeScope m_BulletSpawnerLifetime;
        
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("[MainLifetimeScope] Configure");
            
            builder.RegisterComponentInHierarchy<GameManager>().AsImplementedInterfaces();
            
            builder.Register<ShootInput>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MoveInput>(Lifetime.Singleton).AsImplementedInterfaces();
            
            m_BulletSpawnerLifetime.Configure(builder);
            
            m_CharacterLifetimeScope.Configure(builder);
            m_UILifetimeScope.Configure(builder);
        }
    }
}