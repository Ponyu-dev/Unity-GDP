using _ECS_RTS.Scripts.EcsEngine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.DI
{
    public class RtsEntryPoint : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            Random.InitState(System.Environment.TickCount);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EcsStartup>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}