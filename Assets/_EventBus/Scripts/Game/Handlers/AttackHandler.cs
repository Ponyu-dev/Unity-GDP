using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Players.Components;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class AttackHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        public AttackHandler(EventBus eventBus)
        {
            Debug.Log("AttackHandler Constructor");
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            Debug.Log("AttackHandler Initialize");
            _eventBus.Subscribe<AttackedEvent>(OnHeroAttacked);
        }

        public void Dispose()
        {
            Debug.Log("AttackHandler Dispose");
            _eventBus.Unsubscribe<AttackedEvent>(OnHeroAttacked);
        }

        private void OnHeroAttacked(AttackedEvent evt)
        {
            Debug.Log("AttackHandler Dispose");
            // Обработка логики атаки, например, ответный удар
            if (evt.Target.TryGetComponent(out AttackComponent targetAttack))
            {
                // Ответный удар от цели обратно атакующему
                _eventBus.RaiseEvent(new AttackedEvent(evt.Target, evt.Attacker));
            }
        }
    }
}