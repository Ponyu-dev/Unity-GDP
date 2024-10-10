using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Abilities;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Presenters;
using _EventBus.Scripts.Players.Abilities.Base;
using Cysharp.Threading.Tasks;
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

        private async UniTask OnHeroTurnEnded(TurnEndedEvent evt)
        {
            Debug.Log($"[TurnEndedHandler] OnHeroTurnEnded {evt.Current.HeroType}");

            if (_heroFactory.HasEntity(evt.Current.HeroType))
            {
                if (evt.Current.TryGetComponent<IAbility>(out var ability) &&
                    ability is IAbilityTurnEnd)
                {
                    await _eventBus.RaiseEvent(new AbilityTurnEndEvent(evt.Current));
                    await UniTask.Delay(1000);
                }

                await _eventBus.RaiseEvent(new AbilityHealingGiftEvent(evt.Current));

                if (evt.Current.TryGetComponent<IHeroPresenter>(out var presenter))
                    presenter.SetActive(false);
            }

            await _eventBus.RaiseEvent(new NextHeroEvent());
        }
    }
}