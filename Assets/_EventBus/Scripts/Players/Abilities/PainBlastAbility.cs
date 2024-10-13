using _EventBus.Scripts.Players.Abilities.Base;

namespace _EventBus.Scripts.Players.Abilities
{
    //Каждый раз, когда получает урон, наносит всем 1 ед урона.
    public class PainBlastAbility : IAbilityDealDamage
    {
        public readonly int Damage = 1;
    }
}