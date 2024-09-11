using System;
using ShootEmUp;
using UnityEngine;
using VContainer;

namespace _ShootEmUp.Scripts.VContainer
{
    [Serializable]
    public sealed class EnemySpawnerLifetimeScope
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Transform worldTransform;
        
        [Header("BulletConfig")]
        [SerializeField] private BulletConfig bulletConfig;

        [Header("Pool")]
        [SerializeField] private Enemy prefab;
        [SerializeField] private Transform container;
        [SerializeField] private int initialCount = 7;
        [SerializeField] private bool autoExpand = false;
        
        public void Configure(IContainerBuilder builder)
        {
            Debug.Log("[EnemySpawnerLifetimeScope] Configure");
            
            builder.Register<EnemySpawner>(Lifetime.Singleton)
                .WithParameter(prefab)
                .WithParameter(initialCount)
                .WithParameter(autoExpand)
                .WithParameter("container", container)
                .WithParameter("worldTransform", worldTransform)
                .WithParameter("enemyPositions", enemyPositions)
                .WithParameter("bulletConfig", bulletConfig)
                .AsImplementedInterfaces();
        }
    }
}