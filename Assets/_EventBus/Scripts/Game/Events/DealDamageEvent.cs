using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие изменения здоровья героя
    public struct DealDamageEvent
    {
        public IHeroEntity Target;
        public int Strength;

        public DealDamageEvent(IHeroEntity target, int strength)
        {
            Target = target;
            Strength = strength;
        }
    }
}