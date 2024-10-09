using System;
using _EventBus.Scripts.Game.Events;
using JetBrains.Annotations;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class AttackHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        public AttackHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AttackedEvent>(OnHeroAttacked);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AttackedEvent>(OnHeroAttacked);
        }

        private async void OnHeroAttacked(AttackedEvent evt)
        {
            await _eventBus.RaiseEvent(new DealDamageEvent(evt.Attacker, evt.Target));
        }
    }
}