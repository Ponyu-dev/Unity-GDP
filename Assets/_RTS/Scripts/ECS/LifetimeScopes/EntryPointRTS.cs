using _RTS.Scripts.ECS.SpawnStrategy.Base;
using _RTS.Scripts.ECS.Systems;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _RTS.Scripts.ECS.DI
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
            
            builder.Register<CubeSpawnSystem>(Lifetime.Singleton)
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