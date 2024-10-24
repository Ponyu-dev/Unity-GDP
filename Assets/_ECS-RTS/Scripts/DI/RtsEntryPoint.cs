using _ECS_RTS.Scripts.EcsEngine;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.DI
{
    public class RtsEntryPoint : LifetimeScope
    {
        [BoxGroup("Team Red"), SerializeField] private PoolEnemyConfigure poolEnemyRedConfigure;
        [BoxGroup("Team Blue"), SerializeField] private PoolEnemyConfigure poolEnemyBlueConfigure;
        
        protected override void Awake()
        {
            base.Awake();
            Random.InitState(System.Environment.TickCount);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EntityManager>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<EcsStartup>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            poolEnemyRedConfigure.Configure(builder);
            poolEnemyBlueConfigure.Configure(builder);
        }
    }
}