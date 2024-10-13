namespace _EventBus.Scripts.Players.Abilities.Base
{
    public interface IAbility { }
    public interface IAbilityTurnStart : IAbility { }
    public interface IAbilityAttack : IAbility { }
    public interface IAbilityCounterattack : IAbility { }
    public interface IAbilityDealDamage : IAbility { }
    public interface IAbilityTurnEnd : IAbility { }
}