using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Players.Components;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class DealDamageHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        public DealDamageHandler(EventBus eventBus)
        {
            Debug.Log("DealDamageHandler Constructor");
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            Debug.Log("[DealDamageHandler] Initialize");
            _eventBus.Subscribe<DealDamageEvent>(OnDealDamaged);
        }

        public void Dispose()
        {
            Debug.Log("[DealDamageHandler] Dispose");
            _eventBus.Unsubscribe<DealDamageEvent>(OnDealDamaged);
        }

        private void OnDealDamaged(DealDamageEvent evt)
        {
            Debug.Log("[DealDamageHandler] OnDealDamaged");
            if (!evt.Target.TryGetComponent(out HitPointsComponent hitPointsComponent) ||
                !evt.Attacker.TryGetComponent(out AttackComponent attackComponent))
                return;
            
            hitPointsComponent.Value -= attackComponent.AttackValue;

            if (hitPointsComponent.Value <= 0)
               _eventBus.RaiseEvent(new DiedEvent(evt.Target));
            else
            {
                //_eventBus.RaiseEvent(new AttackedAnimEvent(evt.Target, evt.Attacker));
                _eventBus.RaiseEvent(new TurnEndedEvent(evt.Attacker));
            }
        }
    }
}