using System;
using System.Collections.Generic;
using System.Linq;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using _ECS_RTS.Scripts.EcsEngine.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.DI
{
    [Serializable]
    public class PoolEnemyConfigure
    {
        [SerializeField] private TeamType teamType;
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private bool autoExpand;
        [SerializeField] private List<CustomTypePair<EntityType, Entity>> prefabsList;
        [SerializeField] private Transform[] spawnPoints;

        public void Configure(IContainerBuilder builder)
        {
            builder.Register<CollisionComponentPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<EnemiesFactory>(Lifetime.Scoped)
                .WithParameter("teamType", teamType)
                .WithParameter("container", container)
                .WithParameter("worldTransform", worldTransform)
                .WithParameter("autoExpand", autoExpand)
                .WithParameter("army", CustomTypePair<EntityType, Entity>.ConvertPrefabs(prefabsList))
                .WithParameter("spawnPoints", spawnPoints.Select(t => t.position).ToArray())
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}