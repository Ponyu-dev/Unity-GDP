using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие изменения здоровья героя
    public struct DealDamageEvent
    {
        public readonly IHeroEntity Current;
        public readonly int Strength;

        public DealDamageEvent(IHeroEntity current, int strength)
        {
            Current = current;
            Strength = strength;
        }
    }
}