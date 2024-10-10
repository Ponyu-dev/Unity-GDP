using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Presenters;
using _EventBus.Scripts.Players.Abilities;
using _EventBus.Scripts.Players.Components;
using Cysharp.Threading.Tasks;
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

        private async UniTask OnHeroTurnStarted(TurnStartedEvent evt)
        {
            Debug.Log($"[TurnStartedHandler] OnHeroTurnStarted {evt.Current.HeroType}");

            if (evt.Current.TryGetComponent<IHeroPresenter>(out var presenter))
                presenter.SetActive(true);

            await _eventBus.RaiseEvent(new PlaySoundEvent(evt.Current.StartTurnClip()));
            
            if (evt.Current.TryGetComponent<FreezeDebuffComponent>(out var freezeDebuff))
            {
                if (freezeDebuff.ProcessTurn())
                    evt.Current.RemoveComponent<FreezeDebuffComponent>();
            }
            else
            {
                var targetHero = _heroFactory.GetRandomEntity(evt.Current);
                await _eventBus.RaiseEvent(new AttackedEvent(evt.Current, targetHero));
                await UniTask.Delay(1000);
            }
            
            await _eventBus.RaiseEvent(new TurnEndedEvent(evt.Current));
        }
    }
}