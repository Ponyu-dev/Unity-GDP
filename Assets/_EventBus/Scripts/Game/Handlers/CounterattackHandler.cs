using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Players.Abilities;
using _EventBus.Scripts.Players.Abilities.Base;
using _EventBus.Scripts.Players.Components;
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
            if (evt.Target.TryGetComponent<IAbility>(out var ability) &&
                ability is UnpunishedStrikeAbility)
                return;
            
            if (!evt.Attacker.TryGetComponent(out AttackComponent attackComponent))
                return;
            
            await _eventBus.RaiseEvent(new AttackedAnimEvent(evt.Attacker, evt.Target));
            await _eventBus.RaiseEvent(new DealDamageEvent(evt.Target, attackComponent.Value));
        }
    }
}