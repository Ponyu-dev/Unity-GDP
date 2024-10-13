using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Players.Components;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class HealedHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        [Inject]
        public HealedHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<HealedEvent>(OnHealed);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<HealedEvent>(OnHealed);
        }
        
        private void OnHealed(HealedEvent evt)
        {
            if (!evt.Current.TryGetComponent<HitPointsComponent>(out var hitPointsComponent)) return;
            
            Debug.Log($"[HealedHandler] OnHealed {evt.Current.HeroType} {hitPointsComponent.Value}");
            hitPointsComponent.Value += evt.HellingAmount;
            Debug.Log($"[HealedHandler] OnHealed {evt.Current.HeroType} = {hitPointsComponent.Value}");
        }
    }
}