using System;
using _EventBus.Scripts.Players.Abilities.Base;

namespace _EventBus.Scripts.Players.Abilities
{
    //После атаки с 50% вероятностью забирает здоровье, которое отнял у героя противника.
    [Serializable]
    public class LifeStealChanceAbility : IAbilityAttack
    {
        private readonly Random _random = new Random();
        public bool IsSuccessful => _random.NextDouble() < 0.5;
    }
}