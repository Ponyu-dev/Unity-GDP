using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Presenters;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class TurnEndedHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly IHeroFactory _heroFactory;
        
        [Inject]
        public TurnEndedHandler(
            EventBus eventBus,
            IHeroFactory heroFactory)
        {
            _eventBus = eventBus;
            _heroFactory = heroFactory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<TurnEndedEvent>(OnHeroTurnEnded);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<TurnEndedEvent>(OnHeroTurnEnded);
        }

        private void OnHeroTurnEnded(TurnEndedEvent evt)
        {
            Debug.Log($"Test TurnEndedHandler OnHeroTurnEnded {evt.Current.HeroType}");
            if (evt.Current.TryGetComponent<IHeroPresenter>(out var presenter))
                presenter.SetActive(false);
        }
    }
}