using System;
using ShootEmUp;
using UnityEngine;
using VContainer;

namespace _ShootEmUp.Scripts.VContainer
{
    [Serializable]
    public sealed class BulletSpawnerLifetimeScope
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int initialCount = 50;
        [SerializeField] private bool autoExpand = true;
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;
        
        public void Configure(IContainerBuilder builder)
        {
            Debug.Log("[BulletSpawnerLifetimeScope] Configure");

            builder.RegisterInstance(levelBounds).AsImplementedInterfaces();

            builder.Register<BulletSpawner>(Lifetime.Singleton)
                .WithParameter(bulletPrefab)
                .WithParameter(initialCount)
                .WithParameter(autoExpand)
                .WithParameter("container", container)
                .WithParameter("worldTransform", worldTransform)
                .AsImplementedInterfaces();
        }
    }
}