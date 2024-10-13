using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Abilities;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Players.Abilities;
using _EventBus.Scripts.Players.Abilities.Base;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers.Abilities
{
    [UsedImplicitly]
    public class AbilityTurnEndHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly IHeroFactory _heroFactory;
        
        [Inject]
        public AbilityTurnEndHandler(
            EventBus eventBus,
            IHeroFactory heroFactory)
        {
            _eventBus = eventBus;
            _heroFactory = heroFactory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AbilityTurnEndEvent>(OnAbilityTurnEnded);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AbilityTurnEndEvent>(OnAbilityTurnEnded);
        }
        
        private async UniTask OnAbilityTurnEnded(AbilityTurnEndEvent evt)
        {
            if (evt.Current.TryGetComponent<IAbility>(out var ability) &&
                ability is LastStrikeAbility turnEndAbility)
            {
                Debug.Log($"[AbilityTurnEndHandler] LastStrikeAbility {evt.Current.HeroType}");
                await _eventBus.RaiseEvent(new PlaySoundEvent(evt.Current.AbilityClip()));
                var targetHero = _heroFactory.GetRandomEntity(evt.Current);
                await _eventBus.RaiseEvent(new AttackedAnimEvent(evt.Current, targetHero));
                await _eventBus.RaiseEvent(new DealDamageEvent(evt.Current, targetHero, turnEndAbility.Damage));
            }
        }
    }
}