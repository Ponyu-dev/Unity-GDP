using System;
using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.DI
{
    [Serializable]
    public class SfxConfigure
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private bool autoExpand;
        [SerializeField] private List<CustomTypePair<SfxType, GameObject>> prefabsList;

        public void Configure(IContainerBuilder builder)
        {
            builder.Register<SfxFactory>(Lifetime.Scoped)
                .WithParameter("container", container)
                .WithParameter("worldTransform", worldTransform)
                .WithParameter("autoExpand", autoExpand)
                .WithParameter("prefabs", CustomTypePair<SfxType, GameObject>.ConvertPrefabs(prefabsList))
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}