using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Abilities;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Game.Factories;
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
    public class AttackHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly IHeroFactory _heroFactory;
        
        public AttackHandler(
            EventBus eventBus,
            IHeroFactory heroFactory)
        {
            _eventBus = eventBus;
            _heroFactory = heroFactory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AttackedEvent>(OnHeroAttacked);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AttackedEvent>(OnHeroAttacked);
        }

        //TODO Надо подсвечивать как то героя которого будут бить.
        private async UniTask OnHeroAttacked(AttackedEvent evt)
        {
            var target = evt.Target;
            Debug.Log($"[AttackHandler] OnHeroAttacked target {target.HeroType}");
            if (!evt.Attacker.TryGetComponent(out AttackComponent attackComponent))
                return;
            
            if (evt.Attacker.TryGetComponent<IAbility>(out var ability) &&
                ability is RandomTargetAbility { IsSuccessful: true })
            {
                await _eventBus.RaiseEvent(new PlaySoundEvent(evt.Attacker.AbilityClip()));
                target = _heroFactory.GetRandomEntity(evt.Attacker);
            }
            
            Debug.Log($"[AttackHandler] OnHeroAttacked target {target.HeroType}");
            await _eventBus.RaiseEvent(new AttackedAnimEvent(evt.Attacker, target));
            await _eventBus.RaiseEvent(new DealDamageEvent(evt.Attacker, target, attackComponent.Value));

            await _eventBus.RaiseEvent(new AbilityFreezeGripEvent(evt.Attacker, target));
            
            await UniTask.Delay(1000);

            await _eventBus.RaiseEvent(new CounterattackEvent(target, evt.Attacker));
        }
    }
}