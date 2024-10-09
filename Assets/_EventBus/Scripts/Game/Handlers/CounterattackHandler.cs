using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Effects;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    [UsedImplicitly]
    public class CounterattackHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        public CounterattackHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<CounterattackEvent>(OnCounterattack);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<CounterattackEvent>(OnCounterattack);
        }

        private async UniTask OnCounterattack(CounterattackEvent evt)
        {
            await _eventBus.RaiseEvent(new AttackedAnimEvent(evt.Attacker, evt.Target));
            await _eventBus.RaiseEvent(new DealDamageEvent(evt.Attacker, evt.Target));
        }
    }
}