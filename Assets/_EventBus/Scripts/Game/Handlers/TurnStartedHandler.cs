using System;
using _EventBus.Scripts.Game.Events;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class TurnStartedHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        public TurnStartedHandler(EventBus eventBus)
        {
            Debug.Log("TurnStartedHandler Constructor");
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            Debug.Log("TurnStartedHandler Initialize");
            _eventBus.Subscribe<AttackedEvent>(OnHeroTurnStarted);
        }

        public void Dispose()
        {
            Debug.Log("TurnStartedHandler Dispose");
            _eventBus.Unsubscribe<AttackedEvent>(OnHeroTurnStarted);
        }

        private void OnHeroTurnStarted(AttackedEvent evt)
        {
            Debug.Log("TurnStartedHandler OnHeroTurnStarted");
            
            // Обработка логики атаки, например, ответный удар
            /*if (evt.Target.TryGetComponent(out AttackComponent targetAttack))
            {
                // Ответный удар от цели обратно атакующему
                _eventBus.RaiseEvent(new AttackedEvent(evt.Target, evt.Attacker));
            }*/
        }
    }
}