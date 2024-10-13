using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events.Abilities
{
    public struct AbilityHealingGiftEvent
    {
        public readonly IHeroEntity Current;

        public AbilityHealingGiftEvent(IHeroEntity current)
        {
            Current = current;
        }
    }
}