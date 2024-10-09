using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие конца хода героя
    public struct TurnEndedEvent
    {
        public readonly IHeroEntity Current;

        public TurnEndedEvent(IHeroEntity current)
        {
            Current = current;
        }
    }
}