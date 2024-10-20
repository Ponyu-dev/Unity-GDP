using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Handlers;
using _EventBus.Scripts.Game.Handlers.Abilities;
using _EventBus.Scripts.Game.Handlers.Effects;
using _EventBus.Scripts.Game.Managers;
using _EventBus.Scripts.Game.Presenters;
using _EventBus.Scripts.Players.Hero;
using _EventBus.Scripts.Players.Player;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game
{
    public class EntryPoint : LifetimeScope
    {
        [SerializeField] private HeroView prefabHeroView;
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private TimerView timerView;

        [SerializeField] private HeroesConfig allHeroes;
        
        [SerializeField] private PlayerConfig playersConfigRed;
        [SerializeField] private Transform containerPlayerRed;
        
        [SerializeField] private PlayerConfig playersConfigBlue;
        [SerializeField] private Transform containerPlayerBlue;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(allHeroes);
            
            builder.RegisterComponent(mainMenuView)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.RegisterComponent(timerView)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<HeroPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<EventBus>(Lifetime.Singleton).AsSelf();
            builder.Register<AudioManagers>(Lifetime.Singleton).AsSelf();
            
            builder.Register<HeroFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            ConfigureHandlers(builder);
            ConfigureEffectsHandlers(builder);
            ConfigureAbilityHandlers(builder);
                
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
            
            builder.Register<TimerPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<MainMenuPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }

        private void ConfigureHandlers(IContainerBuilder builder)
        {
            builder.Register<TurnStartedHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AttackHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<CounterattackHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<DealDamageHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<HealedHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<DiedHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<TurnEndedHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private void ConfigureEffectsHandlers(IContainerBuilder builder)
        {
            builder.Register<PlaySoundHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AttackedAnimHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
        
        private void ConfigureAbilityHandlers(IContainerBuilder builder)
        {
            builder.Register<AbilityTurnEndHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AbilityLifeStealChanceHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AbilityPainBlastHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AbilityHealingGiftHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AbilityFreezeGripHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}