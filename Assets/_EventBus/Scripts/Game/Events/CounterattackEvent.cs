using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    public struct CounterattackEvent
    {
        public readonly IHeroEntity Attacker;
        public readonly IHeroEntity Target;

        public CounterattackEvent(IHeroEntity attacker, IHeroEntity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}