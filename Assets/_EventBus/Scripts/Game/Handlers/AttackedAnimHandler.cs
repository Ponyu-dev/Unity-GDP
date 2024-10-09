using System;
using _EventBus.Scripts.Game.Events;
using JetBrains.Annotations;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class AttackedAnimHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        public AttackedAnimHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AttackedAnimEvent>(OnHeroAttacked);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AttackedAnimEvent>(OnHeroAttacked);
        }

        private void OnHeroAttacked(AttackedAnimEvent evt)
        {
            //_eventBus.RaiseEvent(new AttackedEvent(evt.Attacker, evt.Target));
        }
    }
}