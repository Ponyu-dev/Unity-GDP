using CubeECS.Scripts.ECS.SpawnStrategy.Base;
using CubeECS.Scripts.ECS.Systems;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CubeECS.Scripts.ECS.DI
{
    public class EntryPointRTS : LifetimeScope
    {
        [SerializeField] private GameObject redCubePrefab;
        [SerializeField] private Transform redContainer;
        [SerializeField] private GameObject blueCubePrefab;
        [SerializeField] private Transform blueContainer;

        private SpawnStrategyInstaller _spawnStrategyInstaller = new();
        
        protected override void Configure(IContainerBuilder builder)
        {
            _spawnStrategyInstaller.Configure(builder);

            builder.Register<MovementSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<RenderSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<SpawnSystem>(Lifetime.Singleton)
                .WithParameter("redContainer", redContainer)
                .WithParameter("blueContainer", blueContainer)
                .WithParameter("redCubePrefab", redCubePrefab)
                .WithParameter("blueCubePrefab", blueCubePrefab)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<GameRunner>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}