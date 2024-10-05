using System;
using _ChestMechanics.Chests.Data;
using _ChestMechanics.Chests.Presenters;
using UnityEngine;
using VContainer;

namespace _ChestMechanics.Chests.System
{
    [Serializable]
    public class ChestsConfigure
    {
        [SerializeField] private Transform containerChests;
        [SerializeField] private ChestsConfig chestsConfig;

        public void Configure(IContainerBuilder builder)
        {
            Debug.Log("ChestsConfigure RegisterInstance chestsConfig");
            builder.RegisterInstance(chestsConfig);
            
            Debug.Log("ChestsConfigure Register ChestPresenter");
            builder.Register<ChestPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();
            
            Debug.Log("ChestsConfigure Register ChestFactory");
            builder.Register<ChestFactory>(Lifetime.Singleton)
                .WithParameter("containerChests", containerChests)
                .AsImplementedInterfaces()
                .AsSelf();
            
            Debug.Log("ChestsConfigure Register ListChestsPresenter");
            builder.Register<ListChestsPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}