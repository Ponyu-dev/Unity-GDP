using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Abilities;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Players.Abilities;
using _EventBus.Scripts.Players.Abilities.Base;
using _EventBus.Scripts.Players.Components;
using Cysharp.Threading.Tasks;
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

        private async UniTask OnDealDamaged(DealDamageEvent evt)
        {
            var target = evt.Target;

            Debug.Log("[DealDamageHandler] OnDealDamaged");
            if (!target.TryGetComponent(out HitPointsComponent hitPointsComponent))
                return;

            if (target.TryGetComponent<IAbility>(out var ability) &&
                ability is DivineShieldAbility)
            {
                target.RemoveComponent<IAbility>();
                await _eventBus.RaiseEvent(new PlaySoundEvent(target.AbilityClip()));
                return;
            }

            hitPointsComponent.Value -= evt.Damage;
            
            await _eventBus.RaiseEvent(new AbilityLifeStealChanceEvent(evt.Attacker, evt.Damage));

            if (hitPointsComponent.Value <= 0)
                await _eventBus.RaiseEvent(new DiedEvent(target));
            else if (hitPointsComponent.IsHitPointsLow())
                await _eventBus.RaiseEvent(new PlaySoundEvent(target.LowHealthClip()));
        }
    }
}