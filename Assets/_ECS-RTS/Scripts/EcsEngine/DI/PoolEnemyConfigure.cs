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
    public class EntityTypeEntityPair
    {
        public EntityType Key;
        public Entity Value;
    }
    
    [Serializable]
    public class PoolEnemyConfigure
    {
        [SerializeField] private TeamType teamType;
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private bool autoExpand;
        [SerializeField] private List<EntityTypeEntityPair> prefabsList;
        [SerializeField] private Transform[] spawnPoints;

        private Dictionary<EntityType, Entity> ConvertPrefabs()
        {
            var prefabsDictionary = new Dictionary<EntityType, Entity>();
            foreach (var pair in prefabsList)
            {
                prefabsDictionary[pair.Key] = pair.Value;
            }

            return prefabsDictionary;
        }

        public void Configure(IContainerBuilder builder)
        {
            builder.Register<CollisionComponentPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces();
            
            builder.Register<EnemiesFactory>(Lifetime.Scoped)
                .WithParameter("teamType", teamType)
                .WithParameter("container", container)
                .WithParameter("worldTransform", worldTransform)
                .WithParameter("autoExpand", autoExpand)
                .WithParameter("prefabs", ConvertPrefabs())
                .WithParameter("spawnPoints", spawnPoints.Select(t => t.position).ToArray())
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}