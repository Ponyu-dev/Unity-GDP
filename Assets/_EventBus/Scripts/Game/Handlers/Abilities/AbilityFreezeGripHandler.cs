using System;
using _EventBus.Scripts.Game.Events.Abilities;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Players.Abilities;
using _EventBus.Scripts.Players.Abilities.Base;
using _EventBus.Scripts.Players.Components;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers.Abilities
{
    [UsedImplicitly]
    public class AbilityFreezeGripHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        [Inject]
        public AbilityFreezeGripHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AbilityFreezeGripEvent>(OnAbilityFreezeGrip);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AbilityFreezeGripEvent>(OnAbilityFreezeGrip);
        }
        
        private void OnAbilityFreezeGrip(AbilityFreezeGripEvent evt)
        {
            if (evt.Attacker.TryGetComponent<IAbility>(out var ability) &&
                ability is FreezeGripAbility freezeGripAbility)
            {
                evt.Target.AddComponent(new FreezeDebuffComponent(freezeGripAbility.CountTurnFreeze));
            }
        }
    }
}