using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Factories;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class TurnStartedHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly IHeroFactory _heroFactory;
        
        [Inject]
        public TurnStartedHandler(
            EventBus eventBus,
            IHeroFactory heroFactory)
        {
            _eventBus = eventBus;
            _heroFactory = heroFactory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<TurnStartedEvent>(OnHeroTurnStarted);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<TurnStartedEvent>(OnHeroTurnStarted);
        }

        private void OnHeroTurnStarted(TurnStartedEvent evt)
        {
            Debug.Log($"Test TurnStartedHandler OnHeroTurnStarted {evt.CurrentHeroEntity.HeroType}");
            
            var targetHero = _heroFactory.GetRandomEntity(evt.CurrentHeroEntity);
            var attackerHero = _heroFactory.GetEntity(evt.CurrentHeroEntity.HeroType);
            _eventBus.RaiseEvent(new AttackedAnimEvent(attackerHero, targetHero));
        }
    }
}