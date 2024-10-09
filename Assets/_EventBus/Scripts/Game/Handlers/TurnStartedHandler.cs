using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Presenters;
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
            Debug.Log($"[TurnStartedHandler] OnHeroTurnStarted {evt.CurrentHeroEntity.HeroType}");

            if (evt.CurrentHeroEntity.TryGetComponent<IHeroPresenter>(out var presenter))
                presenter.SetActive(true);
                
            await _eventBus.RaiseEvent(new PlaySoundEvent(evt.CurrentHeroEntity.StartTurnClip()));
            
            var targetHero = _heroFactory.GetRandomEntity(evt.CurrentHeroEntity);
            var attackerHero = _heroFactory.GetEntity(evt.CurrentHeroEntity.HeroType);
            
            await _eventBus.RaiseEvent(new AttackedEvent(attackerHero, targetHero));
            await UniTask.Delay(1000);
            await _eventBus.RaiseEvent(new TurnEndedEvent(attackerHero));
        }
    }
}