using CubeECS.Scripts.ECS.Spawn;
using CubeECS.Scripts.ECS.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CubeECS.Scripts.ECS.DI
{
    public class EntryPointCube : LifetimeScope
    {
        [SerializeField] private GameObject redCubePrefab;
        [SerializeField] private Transform redContainer;
        [SerializeField] private GameObject blueCubePrefab;
        [SerializeField] private Transform blueContainer;
        
        [SerializeField] private Transform containerBullet;
        [SerializeField] private GameObject bulletPrefab;

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
            
            builder.Register<BulletCollisionSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<DetectorSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<ShotSystem>(Lifetime.Singleton)
                .WithParameter("bulletPrefab", bulletPrefab)
                .WithParameter("container", containerBullet)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<SpawnEnemySystem>(Lifetime.Singleton)
                .WithParameter("redContainer", redContainer)
                .WithParameter("blueContainer", blueContainer)
                .WithParameter("redCubePrefab", redCubePrefab)
                .WithParameter("blueCubePrefab", blueCubePrefab)
                .AsImplementedInterfaces()
                .AsSelf();

            var ecsWorld = new EcsWorld();
            builder.Register<GameRunner>(Lifetime.Singleton)
                .WithParameter("world", ecsWorld)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}