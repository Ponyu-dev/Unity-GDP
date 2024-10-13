using System;
using _EventBus.Scripts.Players.Abilities.Base;

namespace _EventBus.Scripts.Players.Abilities
{
    //С 50% вероятностью наносит урон не тому противнику.
    public class RandomTargetAbility : IAbilityAttack
    {
        private readonly Random _random = new Random();
        public bool IsSuccessful => _random.NextDouble() < 0.5;
    }
}