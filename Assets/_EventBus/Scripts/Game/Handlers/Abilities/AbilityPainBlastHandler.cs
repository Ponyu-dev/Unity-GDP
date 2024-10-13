using System;
using System.Linq;
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
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers.Abilities
{
    [UsedImplicitly]
    public class AbilityPainBlastHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly IHeroFactory _heroFactory;
        
        [Inject]
        public AbilityPainBlastHandler(
            EventBus eventBus,
            IHeroFactory heroFactory)
        {
            _eventBus = eventBus;
            _heroFactory = heroFactory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AbilityPainBlastEvent>(OnAbilityPainBlast);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AbilityPainBlastEvent>(OnAbilityPainBlast);
        }
        
        private async UniTask OnAbilityPainBlast(AbilityPainBlastEvent evt)
        {
            if (evt.Current.TryGetComponent<IAbility>(out var ability) &&
                ability is PainBlastAbility painBlastAbility)
            {
                Debug.Log($"[AbilityPainBlastHandler] PainBlastAbility {evt.Current.HeroType}");
                await _eventBus.RaiseEvent(new PlaySoundEvent(evt.Current.AbilityClip()));

                var opposingTeam = _heroFactory.GetEntitiesByPredicate(it => it.PlayerType != evt.Current.PlayerType).ToList();
                Debug.Log($"[AbilityPainBlastHandler] PainBlastAbility opposingTeam = {opposingTeam.Count}");
                for (int i = 0, count = opposingTeam.Count; i < count; i++)
                {
                    var target = opposingTeam[i];
                    var hitPointsComponent = target.GetComponent<HitPointsComponent>();
                    Debug.Log($"[AbilityPainBlastHandler] PainBlastAbility {target.HeroType} {hitPointsComponent.Value}");
                    //TODO Добавить SFX для отображения урона
                    await _eventBus.RaiseEvent(new DealDamageEvent(evt.Current, target, painBlastAbility.Damage));
                    Debug.Log($"[AbilityPainBlastHandler] PainBlastAbility {target.HeroType} {hitPointsComponent.Value}");
                }
            }
        }
    }
}