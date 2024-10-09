using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие изменения здоровья героя
    public struct DealDamageEvent
    {
        public readonly IHeroEntity Target;
        public readonly int Damage;

        public DealDamageEvent(IHeroEntity target, int damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}