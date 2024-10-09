using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Players.Components;
using Cysharp.Threading.Tasks;
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

        private async UniTask OnHeroAttacked(AttackedEvent evt)
        {
            if (!evt.Attacker.TryGetComponent(out AttackComponent attackComponent))
                return;
            
            await _eventBus.RaiseEvent(new AttackedAnimEvent(evt.Attacker, evt.Target));
            await _eventBus.RaiseEvent(new DealDamageEvent(evt.Target, attackComponent.Value));
            await UniTask.Delay(1000);
            await _eventBus.RaiseEvent(new CounterattackEvent(evt.Target, evt.Attacker));
        }
    }
}