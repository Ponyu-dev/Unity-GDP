using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events.Abilities
{
    public struct AbilityFreezeGripEvent
    {
        public readonly IHeroEntity Attacker;
        public readonly IHeroEntity Target;

        public AbilityFreezeGripEvent(IHeroEntity attacker, IHeroEntity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}