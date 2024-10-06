using System;
using _EventBus.Scripts.Entities.Player;
using _EventBus.Scripts.Game.Presenters;
using UI;
using UnityEngine;
using VContainer;

namespace _EventBus.Scripts.Game.Players
{
    [Serializable]
    public class PlayersConfigure
    {
        [SerializeField] private HeroView prefabHeroView;
        [SerializeField] private PlayerConfig playersRedConfig;
        [SerializeField] private Transform containerPlayerRed;
        
        [SerializeField] private PlayerConfig playersBlueConfig;
        [SerializeField] private Transform containerPlayerBlue;

        public void Configure(IContainerBuilder builder)
        {
            Debug.Log("PlayersConfigure");
            builder.Register<HeroPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<PlayersPresenter>(Lifetime.Singleton)
                .WithParameter("prefabHeroView", prefabHeroView)
                .WithParameter("playersRedConfig", playersRedConfig)
                .WithParameter("containerPlayerRed", containerPlayerRed)
                .WithParameter("playersBlueConfig", playersBlueConfig)
                .WithParameter("containerPlayerBlue", containerPlayerBlue)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}