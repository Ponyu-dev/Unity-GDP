using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    public struct HealedEvent
    {
        public readonly IHeroEntity Current;
        public readonly int HellingAmount;

        public HealedEvent(IHeroEntity current,int hellingAmount)
        {
            Current = current;
            HellingAmount = hellingAmount;
        }
    }
}