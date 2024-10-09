using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие начала хода героя
    public struct TurnStartedEvent
    {
        public readonly IHeroEntity CurrentHeroEntity;

        public TurnStartedEvent(IHeroEntity currentHeroEntity)
        {
            CurrentHeroEntity = currentHeroEntity;
        }
    }
}