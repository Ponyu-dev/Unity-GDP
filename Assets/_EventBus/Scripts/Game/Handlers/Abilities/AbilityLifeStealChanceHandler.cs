using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Abilities;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Players.Abilities;
using _EventBus.Scripts.Players.Abilities.Base;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers.Abilities
{
    [UsedImplicitly]
    public class AbilityLifeStealChanceHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        [Inject]
        public AbilityLifeStealChanceHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AbilityLifeStealChanceEvent>(OnAbilityLifeStealChance);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AbilityLifeStealChanceEvent>(OnAbilityLifeStealChance);
        }
        
        private async UniTask OnAbilityLifeStealChance(AbilityLifeStealChanceEvent evt)
        {
            if (evt.Current.TryGetComponent<IAbility>(out var ability) &&
                ability is LifeStealChanceAbility { IsSuccessful: true })
            {
                await _eventBus.RaiseEvent(new HealedEvent(evt.Current, evt.LifeStealAmount));
                await _eventBus.RaiseEvent(new PlaySoundEvent(evt.Current.AbilityClip()));
            }
        }
    }
}