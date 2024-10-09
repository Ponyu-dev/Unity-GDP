using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Handlers;
using _EventBus.Scripts.Game.Handlers.Effects;
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
            
            builder.Register<HeroPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<EventBus>(Lifetime.Singleton).AsSelf();
            builder.Register<AudioManagers>(Lifetime.Singleton).AsSelf();
            
            builder.Register<HeroFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            ConfigureHandlers(builder);
                
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

        private void ConfigureHandlers(IContainerBuilder builder)
        {
            builder.Register<PlaySoundHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<TurnStartedHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AttackHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<CounterattackHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AttackedAnimHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<DealDamageHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<DiedHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<TurnEndedHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

        }
    }
}