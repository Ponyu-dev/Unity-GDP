using ShootEmUp;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    public class MainLifetimeScope : LifetimeScope
    {
        [SerializeField] private BulletSpawner bulletSpawner;
        
        [SerializeField] private UILifetimeScope m_UILifetimeScope;
        [SerializeField] private CharacterLifetimeScope m_CharacterLifetimeScope;
        
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("[MainLifetimeScope] Configure");

            builder.RegisterInstance(bulletSpawner).AsImplementedInterfaces();
            
            builder.RegisterComponentInHierarchy<GameManager>().AsImplementedInterfaces();
            
            builder.Register<ShootInput>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MoveInput>(Lifetime.Singleton).AsImplementedInterfaces();
            
            m_CharacterLifetimeScope.Configure(builder);
            
            m_UILifetimeScope.Configure(builder);
        }
    }
}