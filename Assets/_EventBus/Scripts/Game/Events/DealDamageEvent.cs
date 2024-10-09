using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие изменения здоровья героя
    public struct DealDamageEvent
    {
        public readonly IHeroEntity Attacker;
        public readonly IHeroEntity Target;

        public DealDamageEvent(IHeroEntity attacker, IHeroEntity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}