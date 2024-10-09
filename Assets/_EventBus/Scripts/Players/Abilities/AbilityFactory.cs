using System;
using System.Collections.Generic;

namespace _EventBus.Scripts.Players.Abilities
{
    internal static class AbilityFactory
    {
        // Статический словарь для сопоставления AbilityType с функцией создания объекта
        private static readonly Dictionary<AbilityType, Func<IAbility>> _abilityFactory = new()
        {
            { AbilityType.LastStrikeAbility, () => new LastStrikeAbility() },
            { AbilityType.UnpunishedStrikeAbility, () => new UnpunishedStrikeAbility() },
            { AbilityType.RandomTargetAbility, () => new RandomTargetAbility() },
            { AbilityType.LifeStealChanceAbility, () => new LifeStealChanceAbility() },
            { AbilityType.DivineShieldAbility, () => new DivineShieldAbility() },
            { AbilityType.FreezeGripAbility, () => new FreezeGripAbility() },
            { AbilityType.HealingGiftAbility, () => new HealingGiftAbility() },
            { AbilityType.PainBlastAbility, () => new PainBlastAbility() }
        };

        // Метод для получения способности по AbilityType
        public static IAbility GetAbility(AbilityType abilityType)
        {
            if (_abilityFactory.TryGetValue(abilityType, out var createAbility))
            {
                return createAbility();
            }

            throw new ArgumentOutOfRangeException($"Unknown ability type: {abilityType}");
        }
    }

}