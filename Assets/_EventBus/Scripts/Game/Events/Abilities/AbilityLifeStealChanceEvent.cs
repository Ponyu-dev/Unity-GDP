using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events.Abilities
{
    public struct AbilityLifeStealChanceEvent
    {
        public readonly IHeroEntity Current;
        public readonly int LifeStealAmount;

        public AbilityLifeStealChanceEvent(IHeroEntity current, int lifeStealAmount)
        {
            Current = current;
            LifeStealAmount = lifeStealAmount;
        }
    }
}