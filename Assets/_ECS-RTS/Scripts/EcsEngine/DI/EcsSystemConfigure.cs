using Leopotam.EcsLite.Entities;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.DI
{
    public class EcsSystemConfigure
    {
        public void Configure(IContainerBuilder builder)
        {
            builder.Register<EntityManager>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<EcsStartup>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}