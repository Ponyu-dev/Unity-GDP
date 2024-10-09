using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие смерти героя
    public struct DiedEvent
    {
        public readonly IHeroEntity Target;

        public DiedEvent(IHeroEntity target)
        {
            Target = target;
        }
    }
}