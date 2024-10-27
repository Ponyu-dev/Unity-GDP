using System;
using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Services;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.DI
{
    [Serializable]
    public class ArrowPoolConfigure
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private bool autoExpand;
        [SerializeField] private GameObject arrow;

        public void Configure(IContainerBuilder builder)
        {
            builder.Register<ArrowFactory>(Lifetime.Scoped)
                .WithParameter("container", container)
                .WithParameter("worldTransform", worldTransform)
                .WithParameter("autoExpand", autoExpand)
                .WithParameter("prefabArrow", arrow)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}