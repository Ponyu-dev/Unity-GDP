using _ECS_RTS.Scripts.EcsEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.DI
{
    public class RtsEntryPoint : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EcsStartup>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}