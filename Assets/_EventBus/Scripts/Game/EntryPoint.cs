using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Managers;
using _EventBus.Scripts.Game.Presenters;
using _EventBus.Scripts.Players.Hero;
using _EventBus.Scripts.Players.Player;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game
{
    public class EntryPoint : LifetimeScope
    {
        [SerializeField] private HeroView prefabHeroView;

        [SerializeField] private HeroesConfig allHeroes;
        
        [SerializeField] private PlayerConfig playersConfigRed;
        [SerializeField] private Transform containerPlayerRed;
        
        [SerializeField] private PlayerConfig playersConfigBlue;
        [SerializeField] private Transform containerPlayerBlue;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(allHeroes);
            
            builder.Register<EventBus>(Lifetime.Singleton).AsSelf();
            
            builder.Register<HeroPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<HeroFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<PlayerFactory>(Lifetime.Singleton)
                .WithParameter("prefabHeroView", prefabHeroView)
                .WithParameter("playersConfigRed", playersConfigRed)
                .WithParameter("containerPlayerRed", containerPlayerRed)
                .WithParameter("playersConfigBlue", playersConfigBlue)
                .WithParameter("containerPlayerBlue", containerPlayerBlue)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<TurnManager>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<GameManager>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}