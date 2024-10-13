using System;
using System.Linq;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Abilities;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Players.Abilities;
using _EventBus.Scripts.Players.Abilities.Base;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers.Abilities
{
    [UsedImplicitly]
    public class AbilityHealingGiftHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly IHeroFactory _heroFactory;
        
        [Inject]
        public AbilityHealingGiftHandler(
            EventBus eventBus,
            IHeroFactory heroFactory)
        {
            _eventBus = eventBus;
            _heroFactory = heroFactory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AbilityHealingGiftEvent>(OnAbilityHealingGift);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AbilityHealingGiftEvent>(OnAbilityHealingGift);
        }
        
        private async UniTask OnAbilityHealingGift(AbilityHealingGiftEvent evt)
        {
            // Получаем всех героев на стороне игрока, который завершил ход
            var playerHeroes = _heroFactory.GetEntitiesByPredicate(it => it.PlayerType == evt.Current.PlayerType).ToList();
            
            foreach (var hero in playerHeroes)
            {
                // Проверка, есть ли у героя способность лечить случайного союзника
                if (!hero.TryGetComponent<IAbility>(out var ability) ||
                    ability is not HealingGiftAbility healingGiftAbility) continue;
                
                // Выбираем случайного союзника
                var randomAlly = playerHeroes.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
                // Применяем лечение
                if (randomAlly != null)
                    await _eventBus.RaiseEvent(new HealedEvent(randomAlly, healingGiftAbility.HealingAmount));
            }
        }
    }
}