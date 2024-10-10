using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events.Abilities
{
    public struct AbilityPainBlastEvent
    {
        public readonly IHeroEntity Current;

        public AbilityPainBlastEvent(IHeroEntity current)
        {
            Current = current;
        }
    }
}