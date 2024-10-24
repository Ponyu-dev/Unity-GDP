using _ECS_RTS.Scripts.EcsEngine.Systems;
using _ECS_RTS.Scripts.EcsEngine.Systems.Spawns;
using Leopotam.EcsLite.Entities;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.DI
{
    public class EcsSystemConfigure
    {
        public void Configure(IContainerBuilder builder)
        {
            builder.Register<FirstArmySpawnSystem>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<EnemyDestroySystem>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            builder.Register<EntityManager>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<EcsStartup>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}