using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events.Abilities
{
    public struct AbilityTurnEndEvent
    {
        public readonly IHeroEntity Current;

        public AbilityTurnEndEvent(IHeroEntity current)
        {
            Current = current;
        }
    }
}