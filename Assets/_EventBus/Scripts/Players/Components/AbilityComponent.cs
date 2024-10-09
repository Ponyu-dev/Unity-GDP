using _EventBus.Scripts.Players.Abilities;

namespace _EventBus.Scripts.Players.Components
{
    public struct AbilityComponent
    {
        public IAbility Ability;

        public AbilityComponent(IAbility ability)
        {
            Ability = ability;
        }
    }
}