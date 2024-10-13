using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие конца хода
    public struct TurnEndedEvent
    {
        public IHeroEntity Current { get; private set; } 
        
        public TurnEndedEvent(IHeroEntity current)
        {
            Current = current;
        }
    }
}